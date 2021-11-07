using UnityEngine;

namespace Obsidize.FastNoise
{
	/// <summary>
	/// Inverse of the suppressor module that amplifies values within the target range.
	/// </summary>
	[CreateAssetMenu(menuName = "Fast Noise/Modules/Amplifier", fileName = "FastNoiseAmplifier")]
	public class FastNoiseAmplifier : FastNoisePipe
	{

		[SerializeField] [MinMax(0f, 1f)] private MinMaxRange _trim;
		[SerializeField] [Range(0f, 100f)] private float _amplifierValue;

		public MinMaxRange Trim => _trim;
		public float AmplifierValue => _amplifierValue;

		public override float TransformNoise(float input)
		{
			return _trim.Contains(input) ? (input * AmplifierValue) : input;
		}
	}
}
