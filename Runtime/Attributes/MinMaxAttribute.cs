using UnityEngine;

namespace Obsidize.FastNoise
{
	/// <summary>
	/// Loosely based on https://github.com/GucioDevs/SimpleMinMaxSlider/tree/upm
	/// </summary>
	public class MinMaxAttribute : PropertyAttribute
	{

		public readonly float min;
		public readonly float max;

		public MinMaxAttribute(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		public float Clamp(float value)
		{
			return Mathf.Clamp(value, min, max);
		}
	}
}
