using System.Collections.Generic;

namespace Obsidize.FastNoise
{
	public class FastNoiseAggregatorContext<T> : FastNoiseContext where T : IFastNoiseAggregatorContextSource
	{

		private readonly IFastNoiseAggregatorContextOperator<T> _aggregatorOperator;

		public IFastNoiseAggregatorContextOperator<T> AggregatorOperator => _aggregatorOperator;
		public IReadOnlyCollection<T> Sources => AggregatorOperator?.Sources ?? null;
		public int SourceCount => Sources?.Count ?? 0;
		public bool HasSources => SourceCount > 0;

		public FastNoiseAggregatorContext(
			IFastNoiseAggregatorContextOperator<T> aggregatorOperator
		)
		{
			_aggregatorOperator = aggregatorOperator;
		}

		public override void SetSeed(int seed)
		{
			if (!HasSources)
			{
				return;
			}

			foreach (var source in AggregatorOperator.Sources)
			{
				if (source == null || source.Context == null) continue;
				source.Context.SetSeed(seed);
			}
		}

		public override float GetNoise(float x, float y)
		{

			float result = 0f;

			if (!HasSources)
			{
				return result;
			}

			float contextNoise;

			foreach (var source in AggregatorOperator.Sources)
			{
				if (source == null || source.Context == null) continue;
				contextNoise = source.Context.GetNoise(x, y);
				result = source.CombineNoise(result, contextNoise, x, y);
			}

			return AggregatorOperator.NormalizeCombinedNoise2D(result);
		}

		public override float GetNoise(float x, float y, float z)
		{

			float result = 0f;

			if (!HasSources)
			{
				return result;
			}

			float contextNoise;

			foreach (var source in AggregatorOperator.Sources)
			{
				if (source == null || source.Context == null) continue;
				contextNoise = source.Context.GetNoise(x, y);
				result = source.CombineNoise(result, contextNoise, x, y, z);
			}

			return AggregatorOperator.NormalizeCombinedNoise3D(result);
		}
	}
}