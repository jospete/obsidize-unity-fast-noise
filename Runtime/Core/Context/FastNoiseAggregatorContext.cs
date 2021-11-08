using System;
using System.Collections;
using System.Collections.Generic;

namespace Obsidize.FastNoise
{
	public class FastNoiseAggregatorContext : FastNoiseContext, IReadOnlyCollection<IFastNoiseAggregatorContextSource>
	{

		private readonly IFastNoiseAggregatorContextSource[] _sources;
		private readonly IFastNoiseAggregatorContextOperator _aggregatorOperator;
		private readonly IReadOnlyCollection<IFastNoiseAggregatorContextSource> _readonlySources;

		public int Count => _readonlySources.Count;

		public FastNoiseAggregatorContext(
			IFastNoiseAggregatorContextSource[] sources,
			IFastNoiseAggregatorContextOperator aggregatorOperator
		)
		{
			_sources = sources;
			_aggregatorOperator = aggregatorOperator;
			_readonlySources = Array.AsReadOnly(_sources);
		}

		public IEnumerator<IFastNoiseAggregatorContextSource> GetEnumerator()
		{
			return _readonlySources.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _readonlySources.GetEnumerator();
		}

		public override void SetSeed(int seed)
		{
			foreach (var source in this) source?.SetSeed(seed);
		}

		public override float GetNoise(float x, float y)
		{

			float result = 0f;

			foreach (var source in this)
			{
				result = source.CombineNoise(result, x, y);
			}

			return _aggregatorOperator.NormalizeAggregatedNoise2D(result);
		}

		public override float GetNoise(float x, float y, float z)
		{

			float result = 0f;

			foreach (var source in this)
			{
				result = source.CombineNoise(result, x, y, z);
			}

			return _aggregatorOperator.NormalizeAggregatedNoise3D(result);
		}
	}
}