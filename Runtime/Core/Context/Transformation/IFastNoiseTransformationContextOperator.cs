namespace Obsidize.FastNoise
{
	public interface IFastNoiseTransformationContextOperator
	{
		float TransformNoise2D(float noiseValue, float x, float y);
		float TransformNoise3D(float noiseValue, float x, float y, float z);
	}
}
