namespace Obsidize.FastNoise
{
	public interface IFastNoiseAggregatorContextSource
	{
		int Index { get; }
		IFastNoiseContext Context { get; }
		float CombineNoise(float accumulator, float contextNoise, float x, float y);
		float CombineNoise(float accumulator, float contextNoise, float x, float y, float z);
	}
}
