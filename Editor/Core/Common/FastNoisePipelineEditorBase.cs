namespace Obsidize.FastNoise.Editor
{
	public abstract class FastNoisePipelineEditorBase : FastNoiseModuleEditorBase
	{

		public FastNoisePipeline Pipeline => Module as FastNoisePipeline;

		public void NormalizePipelineLayers()
		{
			Pipeline?.CheckForLayerDesync();
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			NormalizePipelineLayers();
		}
	}
}
