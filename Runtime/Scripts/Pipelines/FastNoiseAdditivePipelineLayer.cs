namespace Obsidize.FastNoise
{
	public class FastNoiseAdditivePipelineLayer : FastNoisePipelineLayer
	{

		public override float CombineNoise(float accumulator, float contextNoise, float x, float y)
		{
			return accumulator + contextNoise;
		}

		public override float CombineNoise(float accumulator, float contextNoise, float x, float y, float z)
		{
			return accumulator + contextNoise;
		}
	}
}
