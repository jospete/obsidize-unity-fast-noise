namespace Obsidize.FastNoise
{
	public class FastNoiseSourceContext : FastNoiseContext
	{

		protected readonly FastNoiseLite fnl = new FastNoiseLite();

		private bool _useDomainWarp;

		public override void SetSeed(int seed)
		{
			fnl.SetSeed(seed);
		}

		public override float GetNoise(float x, float y)
		{
			return fnl.At(x, y, _useDomainWarp);
		}

		public override float GetNoise(float x, float y, float z)
		{
			return fnl.At(x, y, z, _useDomainWarp);
		}

		public void SetOptions(FastNoiseOptions options)
		{

			if (options == null) return;

			_useDomainWarp = options.UseDomainWarp;
			fnl.SetOptions(options);
		}
	}
}
