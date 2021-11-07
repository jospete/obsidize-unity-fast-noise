using UnityEngine;

namespace Obsidize.FastNoise
{

	[CreateAssetMenu(menuName = "Fast Noise/Pipeline", fileName = "FastNoisePipeline")]
	public class FastNoisePipeline : FastNoiseModule
	{

		[SerializeField] private FastNoisePipelineEntry[] _layers;
		[HideInInspector] [SerializeField] private float _totalInfluence;

		public int LayerCount => _layers?.Length ?? 0;
		public bool HasLayers => LayerCount > 0;
		public float TotalInfluence => _totalInfluence;

		public bool ContainsModule(FastNoiseModule module)
		{
			if (!HasLayers) return false;

			foreach (var layer in _layers)
			{
				if (layer == null) continue;
				if (layer.ContainsModule(module)) return true;
			}

			return false;
		}

		public override void SetSeed(int seed)
		{
			if (!HasLayers) return;

			foreach (var layer in _layers)
			{
				layer?.SetSeed(seed);
			}
		}

		public override float GetNoise(float x, float y)
		{

			float result = 0f;

			foreach (var layer in _layers)
			{
				result += layer.GetInfluenceNoise(TotalInfluence, x, y);
			}

			return result / _layers.Length;
		}

		public override float GetNoise(float x, float y, float z)
		{
			float result = 0f;

			foreach (var layer in _layers)
			{
				result += layer.GetInfluenceNoise(TotalInfluence, x, y, z);
			}

			return result / _layers.Length;
		}

		public override void DrawPreview(Texture2D texture)
		{
			if (HasLayers) base.DrawPreview(texture);
		}

		private void OnValidate()
		{
			Validate();

			if (!HasLayers) return;

			var influence = 0f;

			foreach (var layer in _layers)
			{
				if (layer == null) continue;
				layer.Validate(this);
				influence += layer.influence;
			}

			_totalInfluence = influence;
		}
	}
}
