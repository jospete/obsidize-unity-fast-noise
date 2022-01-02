using Obsidize.RangeInput;
using UnityEngine;

namespace Obsidize.FastNoise
{
	/// <summary>
	/// Inverse of the suppressor module that amplifies values within the target range.
	/// </summary>
	[CreateAssetMenu(menuName = "Fast Noise/Modules/Amplifier", fileName = "FastNoiseAmplifier")]
	public class FastNoiseAmplifier : FastNoisePipe
	{

		private const float min = FastNoiseLiteExtensions.ofnlMin;
		private const float max = FastNoiseLiteExtensions.ofnlMax;

		[SerializeField] [MinMax(min, max)] private MinMaxRange _trim = new MinMaxRange(min, max);
		[SerializeField] [Range(min, max)] private float _upperBound = max;
		[SerializeField] [Range(min, max)] private float _growth = max;

		public MinMaxRange Trim => _trim;
		public float UpperBound => _upperBound;
		public float Growth => _growth;

		public override float TransformNoise(float input)
		{
			return Trim.Contains(input) ? Mathf.Lerp(input, UpperBound, Growth) : input;
		}
	}
}
