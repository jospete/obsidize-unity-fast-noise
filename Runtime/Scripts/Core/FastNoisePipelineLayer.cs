namespace Obsidize.FastNoise
{
	public abstract class FastNoisePipelineLayer : IFastNoiseAggregatorContextSource
	{

		private FastNoiseModule _module;
		private IFastNoiseContext _context;
		private int _index;

		public FastNoiseModule Module => _module;
		public IFastNoiseContext Context => _context;
		public int Index => _index;

		public abstract float CombineNoise(float accumulator, float contextNoise, float x, float y);
		public abstract float CombineNoise(float accumulator, float contextNoise, float x, float y, float z);

		public void Initialize(FastNoiseModule module, int index)
		{
			_module = module;
			_index = index;
			_context = module != null ? module.CreateContext() : null;
		}
	}
}
