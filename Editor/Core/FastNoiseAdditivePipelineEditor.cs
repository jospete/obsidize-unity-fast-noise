using UnityEditor;

namespace Obsidize.FastNoise.Editor
{
	[CustomEditor(typeof(FastNoiseAdditivePipeline))]
	public class FastNoiseAdditivePipelineEditor : FastNoisePipelineEditorBase
	{

		private FastNoiseAdditivePipeline _pipeline;

		public override FastNoiseModule Module => _pipeline;

		public override void OnInspectorGUI()
		{
			_pipeline = target as FastNoiseAdditivePipeline;
			base.OnInspectorGUI();
		}
	}
}
