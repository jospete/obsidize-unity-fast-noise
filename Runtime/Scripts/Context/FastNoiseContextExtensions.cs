using UnityEngine;

namespace Obsidize.FastNoise
{
	public static class FastNoiseContextExtensions
	{

		// Allows for simple 1D noise queries
		public static float GetNoise(this IFastNoiseContext context, float offset)
		{
			return context.GetNoise(offset, 0f);
		}

		public static float GetNoise(this IFastNoiseContext context, Vector2 position)
		{
			return context.GetNoise(position.x, position.y);
		}

		public static float GetNoise(this IFastNoiseContext context, Vector2Int position)
		{
			return context.GetNoise(position.x, position.y);
		}

		public static float GetNoise(this IFastNoiseContext context, Vector3 position)
		{
			return context.GetNoise(position.x, position.y, position.z);
		}

		public static float GetNoise(this IFastNoiseContext context, Vector3Int position)
		{
			return context.GetNoise(position.x, position.y, position.z);
		}

		public static FastNoiseTransformationContext WithNoiseTransformation(
			this IFastNoiseContext context,
			IFastNoiseTransformationContextOperator transformationOperator
		)
		{

			if (context == null || transformationOperator == null)
			{
				return null;
			}

			return new FastNoiseTransformationContext(context, transformationOperator);
		}
	}
}
