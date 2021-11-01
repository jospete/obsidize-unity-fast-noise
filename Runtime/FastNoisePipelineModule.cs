using UnityEngine;

namespace Obsidize.FastNoise
{
	public abstract class FastNoisePipelineModule : ScriptableObject
	{
		public abstract void SetSeed(int seed);
		public abstract float GetNoise(float x, float y);
		public abstract float GetNoise(float x, float y, float z);
	}
}
