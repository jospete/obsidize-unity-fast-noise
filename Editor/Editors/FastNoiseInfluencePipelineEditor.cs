using UnityEngine;
using UnityEditor;

namespace Obsidize.FastNoise.EditorTools
{
	[CustomEditor(typeof(FastNoiseInfluencePipeline))]
	public class FastNoiseInfluencePipelineEditor : FastNoiseModuleEditorBase
	{

		private FastNoiseInfluencePipeline _pipeline;
		private bool _didUpdateProperties;

		public override FastNoiseModule Module => _pipeline;

		private void ShowLayerInfluenceEditor(int index)
		{

			var layer = _pipeline.GetLayerAt(index);
			if (layer == null) return;

			_pipeline.SetLayerInfluence(
				index,
				EditorGUILayout.FloatField(layer.name, _pipeline.GetLayerInfluence(index))
			);
		}

		private void ShowLayerInfluenceList()
		{

			var previousWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 200f;

			EditorGUILayout.LabelField("Influence Weights", EditorStyles.boldLabel);

			for (int i = 0; i < _pipeline.LayerCount; i++)
			{
				ShowLayerInfluenceEditor(i);
			}

			EditorGUIUtility.labelWidth = previousWidth;
		}

		public override void OnInspectorGUI()
		{

			_didUpdateProperties = DrawDefaultInspector();
			_pipeline = target as FastNoiseInfluencePipeline;

			if (_pipeline == null)
			{
				return;
			}

			EditorGUILayout.Space();

			ShowLayerInfluenceList();

			EditorGUILayout.Space();
			EditorGUILayout.Space();

			GUI.enabled = false;
			EditorGUILayout.FloatField("Combined Influence", _pipeline.TotalInfluence);
			GUI.enabled = true;

			DrawFastNoisePreviewOptions(_didUpdateProperties);
		}
	}
}
