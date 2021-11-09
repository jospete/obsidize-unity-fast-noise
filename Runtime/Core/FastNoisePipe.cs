using UnityEngine;

namespace Obsidize.FastNoise
{
	public abstract class FastNoisePipe : FastNoiseModule, IFastNoiseTransformationContextOperator
	{

		[SerializeField] private FastNoiseModule _inputSource;

		public FastNoiseModule InputSource => _inputSource;

		public virtual float TransformNoise(float noiseValue)
		{
			return noiseValue;
		}

		public float TransformNoiseClamped(float noiseValue)
		{
			return Mathf.Clamp01(TransformNoise(noiseValue));
		}

		public virtual float TransformNoise2D(float noiseValue, float x, float y)
		{
			return TransformNoiseClamped(noiseValue);
		}

		public virtual float TransformNoise3D(float noiseValue, float x, float y, float z)
		{
			return TransformNoiseClamped(noiseValue);
		}

		public override FastNoiseContext CreateContext()
		{
			var result = _inputSource != null ? _inputSource.CreateContext() : null;
			if (result != null) result = result.WithNoiseTransformation(this);
			return result;
		}
	}
}