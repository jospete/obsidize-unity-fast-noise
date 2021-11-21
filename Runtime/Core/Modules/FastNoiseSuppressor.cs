using UnityEngine;

namespace Obsidize.FastNoise
{

	/// <summary>
	/// Inverse of the amplifier module that suppresses values outside the target range.
	/// </summary>
	[CreateAssetMenu(menuName = "Fast Noise/Modules/Suppressor", fileName = "FastNoiseSuppressor")]
	public class FastNoiseSuppressor : FastNoisePipe
	{

		private const float min = FastNoiseLiteExtensions.ofnlMin;
		private const float max = FastNoiseLiteExtensions.ofnlMax;

		[SerializeField] [MinMax(min, max)] private MinMaxRange _trim = MinMaxRange.DefaultLerpRange();
		[SerializeField] [Range(min, max)] private float _lowerBound = min;
		[SerializeField] [Range(min, max)] private float _decay = max;

		public MinMaxRange Trim => _trim;
		public float LowerBound => _lowerBound;
		public float Decay => _decay;

		public override float TransformNoise(float input)
		{
			return Trim.Contains(input) ? input : Mathf.Lerp(input, LowerBound, Decay);
		}
	}
}
