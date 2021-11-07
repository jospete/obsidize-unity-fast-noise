using UnityEngine;

namespace Obsidize.FastNoise
{
	/// <summary>
	/// Inverts input values from range [0, 1] to [1, 0], e.g. 0.75 -> 0.25
	/// </summary>
	[CreateAssetMenu(menuName = "Fast Noise/Modules/Inverter", fileName = "FastNoiseInverter")]
	public class FastNoiseInverter : FastNoisePipe
	{

		public override float TransformNoise(float noise)
		{
			return 1f - noise;
		}
	}
}
