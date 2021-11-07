using UnityEditor;

namespace Obsidize.FastNoise.EditorTools
{
	[CustomEditor(typeof(FastNoiseAdditivePipeline))]
	public class FastNoiseAdditivePipelineEditor : FastNoiseModuleEditorBase
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
