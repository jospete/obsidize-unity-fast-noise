using System.Collections.Generic;

namespace Obsidize.FastNoise
{
	public class FastNoiseAggregatorContext<T> : FastNoiseContext where T : IFastNoiseAggregatorContextSource
	{

		private const float min = FastNoiseLiteExtensions.ofnlMin;

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

		protected virtual void SetSourceSeed(T source, int seed)
		{
			source.Context.SetSeed(seed);
		}

		protected virtual float CombineSourceNoise(T source, float accumulator, float x, float y)
		{
			return source.CombineNoise(accumulator, source.Context.GetNoise(x, y), x, y);
		}

		protected virtual float CombineSourceNoise(T source, float accumulator, float x, float y, float z)
		{
			return source.CombineNoise(accumulator, source.Context.GetNoise(x, y, z), x, y, z);
		}

		public override void SetSeed(int seed)
		{

			foreach (var source in AggregatorOperator.Sources)
			{
				SetSourceSeed(source, seed);
			}
		}

		public override float GetNoise(float x, float y)
		{

			float result = min;

			foreach (var source in AggregatorOperator.Sources)
			{
				result = CombineSourceNoise(source, result, x, y);
			}

			return AggregatorOperator.NormalizeCombinedNoise2D(result);
		}

		public override float GetNoise(float x, float y, float z)
		{

			float result = min;

			foreach (var source in AggregatorOperator.Sources)
			{
				result = CombineSourceNoise(source, result, x, y, z);
			}

			return AggregatorOperator.NormalizeCombinedNoise3D(result);
		}
	}
}