using UnityEngine;

namespace Obsidize.FastNoise
{

	[CreateAssetMenu(menuName = "Fast Noise/Pipeline", fileName = "FastNoisePipeline")]
	public class FastNoisePipeline : FastNoisePipelineModule
	{

		[SerializeField] private FastNoisePreviewOptions _preview = new FastNoisePreviewOptions();
		public FastNoisePipelineEntry[] layers;

		[HideInInspector] [SerializeField] private float _totalInfluence;

		public FastNoisePreviewOptions Preview => _preview;
		public bool HasLayers => layers != null && layers.Length > 0;
		public float TotalInfluence => _totalInfluence;

		public void DrawPreview(Texture2D texture)
		{
			if (_preview == null || !HasLayers) return;
			_preview.DrawPreviewTexture(this, texture);
		}

		public bool ContainsModule(FastNoisePipelineModule module)
		{
			if (!HasLayers) return false;

			foreach (var layer in layers)
			{
				if (layer == null) continue;
				if (layer.ContainsModule(module)) return true;
			}

			return false;
		}

		public override void SetSeed(int seed)
		{
			if (!HasLayers) return;

			foreach (var layer in layers)
			{
				layer?.SetSeed(seed);
			}
		}

		public override float GetNoise(float x, float y)
		{

			float result = 0f;

			foreach (var layer in layers)
			{
				result += layer.GetInfluenceNoise(TotalInfluence, x, y);
			}

			return result / layers.Length;
		}

		public override float GetNoise(float x, float y, float z)
		{
			float result = 0f;

			foreach (var layer in layers)
			{
				result += layer.GetInfluenceNoise(TotalInfluence, x, y, z);
			}

			return result / layers.Length;
		}

		private void OnValidate()
		{
			if (!HasLayers) return;

			var influence = 0f;

			foreach (var layer in layers)
			{
				if (layer == null) continue;
				layer.Validate(this);
				influence += layer.influence;
			}

			_totalInfluence = influence;
		}
	}
}
