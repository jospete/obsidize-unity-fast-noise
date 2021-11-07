using UnityEngine;
using UnityEditor;

namespace Obsidize.FastNoise.EditorTools
{
	[CustomEditor(typeof(FastNoisePipeline))]
	public class FastNoisePipelineEditor : FastNoiseModuleEditorBase
	{

		private FastNoisePipeline _pipeline;
		private bool _didUpdateProperties;

		public override FastNoiseModule Module => _pipeline;

		public override void OnInspectorGUI()
		{

			_didUpdateProperties = DrawDefaultInspector();
			_pipeline = target as FastNoisePipeline;

			if (_pipeline == null)
			{
				return;
			}

			GUI.enabled = false;
			EditorGUILayout.FloatField("Combined Influence", _pipeline.TotalInfluence);
			GUI.enabled = true;

			DrawFastNoisePreviewOptions(_didUpdateProperties);
		}
	}
}
