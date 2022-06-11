using UnityEngine;
using UnityEditor;

namespace Obsidize.FastNoise.Editor
{
	[CustomEditor(typeof(FastNoiseInfluencePipeline))]
	public class FastNoiseInfluencePipelineEditor : FastNoisePipelineEditorBase
	{

		private FastNoiseInfluencePipeline _pipeline;
		private bool _didUpdateProperties;

		public override FastNoiseModule Module => _pipeline;

		private bool ShowLayerInfluenceEditor(int index)
		{

			var layer = _pipeline.GetModuleAt(index);
			if (layer == null) return false;

			var currentValue = _pipeline.GetLayerInfluence(index);

			var updateValue = _pipeline.SetLayerInfluence(
				index,
				EditorGUILayout.FloatField(layer.name, currentValue)
			);

			return currentValue != updateValue;
		}

		private bool ShowLayerInfluenceList()
		{

			var previousWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 200f;

			EditorGUILayout.LabelField("Influence Weights", EditorStyles.boldLabel);

			bool didUpdateAny = false;
			bool didUpdateNext;

			for (int i = 0; i < _pipeline.ModuleCount; i++)
			{
				didUpdateNext = ShowLayerInfluenceEditor(i);
				didUpdateAny = didUpdateAny || didUpdateNext;
			}

			EditorGUIUtility.labelWidth = previousWidth;

			return didUpdateAny;
		}

		public override void OnInspectorGUI()
		{

			_didUpdateProperties = DrawDefaultNoiseModuleInspector();
			_pipeline = target as FastNoiseInfluencePipeline;

			if (_pipeline == null)
			{
				return;
			}

			EditorGUILayout.Space();

			if (ShowLayerInfluenceList())
			{
				_didUpdateProperties = true;
				_pipeline.CalculateTotalInfluence();
				EditorUtility.SetDirty(_pipeline);
			}

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			GUI.enabled = false;
			EditorGUILayout.FloatField("Combined Influence", _pipeline.TotalInfluence);
			GUI.enabled = true;

			DrawFastNoisePreviewOptions(_didUpdateProperties);
		}
	}
}
