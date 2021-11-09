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
			Source.SetSeed(seed);
		}

		public override float GetNoise(float x, float y)
		{
			return TransformationOperator.TransformNoise2D(Source.GetNoise(x, y), x, y);
		}

		public override float GetNoise(float x, float y, float z)
		{
			return TransformationOperator.TransformNoise3D(Source.GetNoise(x, y, z), x, y, z);
		}
	}
}
