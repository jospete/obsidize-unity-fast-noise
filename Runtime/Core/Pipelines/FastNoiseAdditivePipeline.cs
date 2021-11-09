using UnityEngine;

namespace Obsidize.FastNoise
{

	[CreateAssetMenu(menuName = "Fast Noise/Pipelines/Additive", fileName = "FastNoiseAdditivePipeline")]
	public class FastNoiseAdditivePipeline : FastNoisePipeline
	{

		protected override FastNoisePipelineLayer CreateLayer()
		{
			return new FastNoiseAdditivePipelineLayer();
		}
	}
}
