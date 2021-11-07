using UnityEngine;
using UnityEditor;

namespace Obsidize.FastNoise.EditorTools
{

	/// <summary>
	/// Loosely based on https://github.com/GucioDevs/SimpleMinMaxSlider/tree/upm
	/// </summary>
	[CustomPropertyDrawer(typeof(MinMaxAttribute))]
	public class MinMaxRangeDrawer : PropertyDrawer
	{

		private const string minPropertyName = "_min";
		private const string maxPropertyName = "_max";

		private const int minFloatInputRect = 0;
		private const int sliderInputRect = 1;
		private const int maxFloatInputRect = 2;
		private const int rectSplitCount = 3;
		private const int splitRectPadding = 5;

		private bool _hasCustomDrawer;

		private Rect[] SplitRect(Rect rectToSplit)
		{

			var rectCount = rectSplitCount;
			Rect[] rects = new Rect[rectCount];

			for (int i = 0; i < rectCount; i++)
			{
				rects[i] = new Rect(
					rectToSplit.position.x + (i * rectToSplit.width / rectCount),
					rectToSplit.position.y,
					rectToSplit.width / rectCount,
					rectToSplit.height
				);
			}

			int sliderWidth = (int)rects[minFloatInputRect].width - 50;

			rects[minFloatInputRect].width -= sliderWidth + splitRectPadding;
			rects[maxFloatInputRect].width -= sliderWidth + splitRectPadding;
			rects[sliderInputRect].x -= sliderWidth;
			rects[sliderInputRect].width += sliderWidth * 2;
			rects[maxFloatInputRect].x += sliderWidth + splitRectPadding;

			return rects;
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUI.GetPropertyHeight(property, label, true);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{

			var isMinMaxStruct = fieldInfo.FieldType == typeof(MinMaxRange);
			var minMaxAttribute = attribute as MinMaxAttribute;
			_hasCustomDrawer = isMinMaxStruct && minMaxAttribute != null;

			if (!_hasCustomDrawer)
			{
				EditorGUI.PropertyField(position, property, label, true);
				return;
			}

			var minProperty = property.FindPropertyRelative(minPropertyName);
			var maxProperty = property.FindPropertyRelative(maxPropertyName);

			var minVal = minProperty.floatValue;
			var maxVal = maxProperty.floatValue;

			// PrefixLabel returns the rect of the right part of the control.
			// It leaves out the label section. We don't have to worry about it. Nice!
			Rect controlRect = EditorGUI.PrefixLabel(position, label);
			Rect[] splittedRect = SplitRect(controlRect);

			label.tooltip = minMaxAttribute.min.ToString("F2") + " to " + minMaxAttribute.max.ToString("F2");

			EditorGUI.BeginChangeCheck();

			minVal = EditorGUI.FloatField(splittedRect[minFloatInputRect], minVal);
			maxVal = EditorGUI.FloatField(splittedRect[maxFloatInputRect], maxVal);

			EditorGUI.MinMaxSlider(
				splittedRect[sliderInputRect],
				ref minVal,
				ref maxVal,
				minMaxAttribute.min,
				minMaxAttribute.max
			);

			minVal = minMaxAttribute.Clamp(minVal);
			maxVal = minMaxAttribute.Clamp(maxVal);

			if (!EditorGUI.EndChangeCheck())
			{
				return;
			}

			minProperty.floatValue = minVal;
			maxProperty.floatValue = maxVal;
			property.serializedObject.ApplyModifiedProperties();
		}
	}
}
