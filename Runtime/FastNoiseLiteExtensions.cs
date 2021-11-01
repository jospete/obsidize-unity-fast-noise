namespace Obsidize.FastNoise
{
	public static class FastNoiseLiteExtensions
	{

		// FastNoiseLite returns in range [-1,1]
		// so we scale the value to be in range [0,1] here for lerp / height map calls.
		private static float ScaleNoiseValue(float v) => (v + 1f) / 2f;

		public static float GetLerpNoise(this FastNoiseLite noise, float x, float y)
		{
			return ScaleNoiseValue(noise.GetNoise(x, y));
		}

		public static float GetLerpNoise(this FastNoiseLite noise, float x, float y, float z)
		{
			return ScaleNoiseValue(noise.GetNoise(x, y, z));
		}

		public static float GetDomainWarpNoise(this FastNoiseLite noise, float x, float y)
		{
			noise.DomainWarp(ref x, ref y);
			return noise.GetNoise(x, y);
		}

		public static float GetDomainWarpNoise(this FastNoiseLite noise, float x, float y, float z)
		{
			noise.DomainWarp(ref x, ref y, ref z);
			return noise.GetNoise(x, y, z);
		}

		public static float GetLerpDomainWarpNoise(this FastNoiseLite noise, float x, float y)
		{
			return ScaleNoiseValue(GetDomainWarpNoise(noise, x, y));
		}

		public static float GetLerpDomainWarpNoise(this FastNoiseLite noise, float x, float y, float z)
		{
			return ScaleNoiseValue(GetDomainWarpNoise(noise, x, y, z));
		}

		// Simplified alias of GetLerpDomainWarpNoise()
		public static float At(this FastNoiseLite noise, float x, float y)
		{
			return GetLerpDomainWarpNoise(noise, x, y);
		}

		// Simplified alias of GetLerpDomainWarpNoise()
		public static float At(this FastNoiseLite noise, float x, float y, float z)
		{
			return GetLerpDomainWarpNoise(noise, x, y, z);
		}

		public static void SetPreviewOptions(this FastNoiseLite noise, FastNoiseOptions config)
		{
			if (noise == null || config == null || config.preview == null) return;

			noise.SetOptions(config);
			noise.SetSeed(config.preview.seed);
		}

		public static void SetOptions(this FastNoiseLite noise, FastNoiseOptions config)
		{
			if (noise == null || config == null) return;

			noise.SetFrequency(config.Frequency);
			noise.SetFractalType(config.FractalType);
			noise.SetFractalLacunarity(config.Lacunarity);
			noise.SetFractalWeightedStrength(config.WeightedStrength);
			noise.SetFractalPingPongStrength(config.PingPongStrength);
			noise.SetCellularDistanceFunction(config.CellularDistanceFunction);
			noise.SetCellularReturnType(config.CellularReturnType);
			noise.SetCellularJitter(config.CellularJitterModifier);
			noise.SetDomainWarpAmp(config.DomainWarpAmp);
			noise.SetNoiseType(config.NoiseType);
			noise.SetFractalOctaves(config.Octaves);
			noise.SetFractalGain(config.Gain);
			noise.SetDomainWarpType(config.DomainWarpType);
			noise.SetRotationType3D(config.RotationType3D);
		}
	}
}
