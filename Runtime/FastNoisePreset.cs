using UnityEngine;

namespace Obsidize.FastNoise
{

	[CreateAssetMenu(menuName = "Fast Noise/Module", fileName = "FastNoiseModule")]
	public class FastNoisePreset : FastNoisePipelineModule
	{

		protected readonly FastNoiseLite noise = new FastNoiseLite();

		[SerializeField] private FastNoisePreviewOptions _preview = new FastNoisePreviewOptions();
		[SerializeField] private FastNoiseOptions _options = new FastNoiseOptions();

		public FastNoisePreviewOptions Preview => _preview;
		public FastNoiseOptions Options => _options;

		private void Awake()
		{
			SyncOptions();
		}

		private void OnValidate()
		{
			if (Preview != null) Preview.Validate();
			if (Options != null) Options.Validate();
		}

		public void SyncOptions()
		{
			noise.SetOptions(Options);
		}

		public override void SetSeed(int seed)
		{
			SyncOptions();
			noise.SetSeed(seed);
		}

		public override float GetNoise(float x, float y)
		{
			return noise.At(x, y, Options.UseDomainWarp);
		}

		public override float GetNoise(float x, float y, float z)
		{
			return noise.At(x, y, z, Options.UseDomainWarp);
		}

		public void DrawPreview(Texture2D texture)
		{
			if (noise == null || Preview == null) return;

			SyncOptions();
			Preview.DrawPreviewTexture(this, texture);
		}
	}
}
