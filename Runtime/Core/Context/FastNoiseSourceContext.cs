namespace Obsidize.FastNoise
{
	public class FastNoiseSourceContext : FastNoiseContext
	{

		protected readonly FastNoiseLite fnl = new FastNoiseLite();

		public override void SetSeed(int seed)
		{
			fnl.SetSeed(seed);
		}

		public void SetOptions(FastNoiseOptions options)
		{
			fnl.SetOptions(options);
		}

		public override float GetNoise(float x, float y)
		{
			return fnl.At(x, y);
		}

		public override float GetNoise(float x, float y, float z)
		{
			return fnl.At(x, y, z);
		}
	}
}
