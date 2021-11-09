using System;

namespace Obsidize.FastNoise
{
	public class FastNoiseInfluencePipelineLayer : FastNoisePipelineLayer
	{

		private readonly Func<int, float> _getInfluence;
		public float InfluenceRatio => _getInfluence(Index);

		public FastNoiseInfluencePipelineLayer(Func<int, float> getInfluence)
		{
			_getInfluence = getInfluence;
		}

		public override float CombineNoise(float accumulator, float contextNoise, float x, float y)
		{
			return accumulator + (contextNoise * InfluenceRatio);
		}

		public override float CombineNoise(float accumulator, float contextNoise, float x, float y, float z)
		{
			return accumulator + (contextNoise * InfluenceRatio);
		}
	}
}
