using UnityEngine;

namespace Obsidize.FastNoise
{

	/// <summary>
	/// Inverse of the amplifier module that suppresses values outside the target range.
	/// </summary>
	[CreateAssetMenu(menuName = "Fast Noise/Modules/Suppressor", fileName = "FastNoiseSuppressor")]
	public class FastNoiseSuppressor : FastNoisePipe
	{

		[SerializeField] [MinMax(0f, 1f)] private MinMaxRange _trim;
		[SerializeField] [Range(0f, 1f)] private float _lowerBound = 0f;
		[SerializeField] [Range(0f, 1f)] private float _decay = 1f;

		public MinMaxRange Trim => _trim;
		public float LowerBound => _lowerBound;
		public float Decay => _decay;

		public override float TransformNoise(float input)
		{
			return _trim.Contains(input) ? input : Mathf.Lerp(input, LowerBound, Decay);
		}
	}
}
