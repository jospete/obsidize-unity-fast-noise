namespace Obsidize.FastNoise
{
	public interface IFastNoiseAggregatorContextOperator
	{
		float NormalizeAggregatedNoise2D(float value);
		float NormalizeAggregatedNoise3D(float value);
	}
}
