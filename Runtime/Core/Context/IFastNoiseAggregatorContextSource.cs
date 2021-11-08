namespace Obsidize.FastNoise
{
	public interface IFastNoiseAggregatorContextSource
	{
		void SetSeed(int seed);
		float CombineNoise(float accumulator, float x, float y);
		float CombineNoise(float accumulator, float x, float y, float z);
	}
}
