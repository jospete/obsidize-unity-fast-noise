using UnityEngine;

namespace Obsidize.FastNoise
{
	/// <summary>
	/// Inverse of the suppressor module that amplifies values within the target range.
	/// </summary>
	[CreateAssetMenu(menuName = "Fast Noise/Modules/Amplifier", fileName = "FastNoiseAmplifier")]
	public class FastNoiseAmplifier : FastNoisePipe
	{

		[SerializeField] [MinMax(0f, 1f)] private MinMaxRange _trim = MinMaxRange.DefaultLerpRange();
		[SerializeField] [Range(0f, 1f)] private float _upperBound = 1f;
		[SerializeField] [Range(0f, 1f)] private float _growth = 1f;

		public MinMaxRange Trim => _trim;
		public float UpperBound => _upperBound;
		public float Growth => _growth;

		public override float TransformNoise(float input)
		{
			return Trim.Contains(input) ? Mathf.Lerp(input, UpperBound, Growth) : input;
		}
	}
}
