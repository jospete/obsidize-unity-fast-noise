using System.Collections.Generic;

namespace Obsidize.FastNoise
{
	public interface IFastNoiseAggregatorContextOperator<T> where T : IFastNoiseAggregatorContextSource
	{
		IReadOnlyCollection<T> Sources { get; }
		float NormalizeCombinedNoise2D(float value);
		float NormalizeCombinedNoise3D(float value);
	}
}
