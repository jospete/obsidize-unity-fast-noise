namespace Obsidize.FastNoise
{
	/// <summary>
	/// Wrapper of the standard FastNoiseAggregatorContext implementation that
	/// performs additional null safety checks.
	/// 
	/// This is best used in the editor to avoid null pointer errors during development.
	/// The standard FastNoiseAggregatorContext implementation should be used during 
	/// gameplay to maximize performance.
	/// </summary>
	public class AdaptiveFastNoiseAggregatorContext<T> : FastNoiseAggregatorContext<T> where T : IFastNoiseAggregatorContextSource
	{
		public AdaptiveFastNoiseAggregatorContext(
			IFastNoiseAggregatorContextOperator<T> aggregatorOperator
		) : base(aggregatorOperator)
		{
		}

		protected override void SetSourceSeed(T source, int seed)
		{
			if (source == null || source.Context == null) base.SetSourceSeed(source, seed);
		}

		protected override float CombineSourceNoise(T source, float accumulator, float x, float y)
		{
			return (source != null && source.Context != null)
				? base.CombineSourceNoise(source, accumulator, x, y)
				: accumulator;
		}

		protected override float CombineSourceNoise(T source, float accumulator, float x, float y, float z)
		{
			return (source != null && source.Context != null)
				? base.CombineSourceNoise(source, accumulator, x, y, z)
				: accumulator;
		}

		public override void SetSeed(int seed)
		{
			if (HasSources) base.SetSeed(seed);
		}

		public override float GetNoise(float x, float y)
		{
			return HasSources ? base.GetNoise(x, y) : 0f;
		}

		public override float GetNoise(float x, float y, float z)
		{
			return HasSources ? base.GetNoise(x, y, z) : 0f;
		}
	}
}
