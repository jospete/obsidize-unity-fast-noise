using UnityEngine;

namespace Obsidize.FastNoise
{
	public abstract class FastNoisePipe : FastNoiseModule
	{

		[SerializeField] private FastNoiseModule _inputSource;

		public FastNoiseModule InputSource => _inputSource;

		public abstract float TransformNoise(float noise);

		public override void SetSeed(int seed)
		{
			InputSource?.SetSeed(seed);
		}

		public override float GetNoise(float x, float y)
		{
			return TransformNoise(InputSource?.GetNoise(x, y) ?? 0f);
		}

		public override float GetNoise(float x, float y, float z)
		{
			return TransformNoise(InputSource?.GetNoise(x, y, z) ?? 0f);
		}
	}
}