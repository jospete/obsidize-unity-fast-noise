using UnityEngine;

namespace Obsidize.FastNoise
{

	[CreateAssetMenu(menuName = "Fast Noise/Pipelines/Additive", fileName = "FastNoiseAdditivePipeline")]
	public class FastNoiseAdditivePipeline : FastNoisePipeline
	{

		public class AggregatorSource : IFastNoiseAggregatorContextSource
		{

			private readonly IFastNoiseContext _context;

			public AggregatorSource(FastNoiseModule module)
			{
				_context = module.CreateContext();
			}

			public float CombineNoise(float accumulator, float x, float y)
			{
				return accumulator + _context.GetNoise(x, y);
			}

			public float CombineNoise(float accumulator, float x, float y, float z)
			{
				return accumulator + _context.GetNoise(x, y, z);
			}

			public void SetSeed(int seed)
			{
				_context.SetSeed(seed);
			}
		}


		protected override void OnValidate()
		{
			base.OnValidate();
		}

		protected override IFastNoiseAggregatorContextSource CreateAggregatorSource(FastNoiseModule module, int index)
		{
			return new AggregatorSource(module);
		}
	}
}
