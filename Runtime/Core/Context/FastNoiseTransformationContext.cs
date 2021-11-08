namespace Obsidize.FastNoise
{
	public class FastNoiseTransformationContext : FastNoiseContext
	{

		private readonly IFastNoiseContext _source;
		private readonly IFastNoiseTransformationContextOperator _transformationOperator;

		public IFastNoiseContext Source => _source;
		public IFastNoiseTransformationContextOperator TransformationOperator => _transformationOperator;

		public FastNoiseTransformationContext(
			IFastNoiseContext source,
			IFastNoiseTransformationContextOperator transformationOperator
		)
		{
			_source = source;
			_transformationOperator = transformationOperator;
		}

		public override void SetSeed(int seed)
		{
			_source.SetSeed(seed);
		}

		public override float GetNoise(float x, float y)
		{
			return _transformationOperator.TransformNoise2D(_source.GetNoise(x, y), x, y);
		}

		public override float GetNoise(float x, float y, float z)
		{
			return _transformationOperator.TransformNoise3D(_source.GetNoise(x, y, z), x, y, z);
		}
	}
}
