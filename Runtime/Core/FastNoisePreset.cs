using UnityEngine;

namespace Obsidize.FastNoise
{

	[CreateAssetMenu(menuName = "Fast Noise/Modules/Preset", fileName = "FastNoisePreset")]
	public class FastNoisePreset : FastNoiseModule
	{

		[SerializeField] private FastNoiseOptions _options = new FastNoiseOptions();

		public FastNoiseOptions Options => _options;

		protected readonly FastNoiseLite noise = new FastNoiseLite();

		private void Awake()
		{
			SyncOptions();
		}

		private void OnValidate()
		{
			Validate();
			Options?.Validate();
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

		public override void DrawPreview(Texture2D texture)
		{
			if (noise == null) return;

			SyncOptions();
			base.DrawPreview(texture);
		}
	}
}
