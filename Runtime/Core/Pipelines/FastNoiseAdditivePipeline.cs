using UnityEngine;

namespace Obsidize.FastNoise
{

	[CreateAssetMenu(menuName = "Fast Noise/Pipelines/Additive", fileName = "FastNoiseAdditivePipeline")]
	public class FastNoiseAdditivePipeline : FastNoisePipeline
	{

		private void OnValidate()
		{
			Validate();
		}

		protected override float ApplyLayerNoise(float currentValue, FastNoisePipelineLayerContext context, float x, float y)
		{
			return currentValue + context.layerNoise;
		}

		protected override float ApplyLayerNoise(float currentValue, FastNoisePipelineLayerContext context, float x, float y, float z)
		{
			return currentValue + context.layerNoise;
		}
	}
}
