namespace Obsidize.FastNoise
{
	public abstract class FastNoiseContext : IFastNoiseContext
	{

		public abstract void SetSeed(int seed);
		public abstract float GetNoise(float x, float y);
		public abstract float GetNoise(float x, float y, float z);
	}
}
