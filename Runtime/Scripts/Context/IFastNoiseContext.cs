namespace Obsidize.FastNoise
{
	public interface IFastNoiseContext
	{
		void SetSeed(int seed);
		float GetNoise(float x, float y);
		float GetNoise(float x, float y, float z);
	}
}
