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
		[SerializeField] [Range(0f, 1f)] private float _suppressionValue;

		public MinMaxRange Trim => _trim;
		public float SuppressionValue => _suppressionValue;

		public override float TransformNoise(float input)
		{
			return _trim.Contains(input) ? input : SuppressionValue;
		}
	}
}
