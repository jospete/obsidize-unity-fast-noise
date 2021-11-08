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

		public virtual float TransformNoise2D(float noiseValue, float x, float y)
		{
			return TransformNoise(noiseValue);
		}

		public virtual float TransformNoise3D(float noiseValue, float x, float y, float z)
		{
			return TransformNoise(noiseValue);
		}

		public override FastNoiseContext CreateContext()
		{
			var result = InputSource?.CreateContext() ?? null;
			if (result != null) result = result.WithNoiseTransformation(this);
			return result;
		}
	}
}