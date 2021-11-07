using UnityEngine;

namespace Obsidize.FastNoise
{

	[System.Serializable]
	public struct MinMaxRange
	{

		[SerializeField] private float _min;
		[SerializeField] private float _max;

		public float Min
		{
			get => _min;
			set => _min = Mathf.Min(value, _max);
		}

		public float Max
		{
			get => _max;
			set => _max = Mathf.Max(value, _min);
		}

		public MinMaxRange(float min, float max)
		{
			_min = Mathf.Min(min, max);
			_max = Mathf.Max(min, max);
		}

		public float Clamp(float value)
		{
			return Mathf.Clamp(value, Min, Max);
		}

		public bool Contains(float value)
		{
			return Min <= value && value <= Max;
		}

		public static MinMaxRange DefaultLerpRange()
		{
			return new MinMaxRange(0f, 1f);
		}
	}
}
