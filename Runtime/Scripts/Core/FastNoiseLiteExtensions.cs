using UnityEngine;

namespace Obsidize.FastNoise
{
	public static class FastNoiseLiteExtensions
	{

		public const float fnlMin = -1f;
		public const float fnlMax = 1f;

		public const float ofnlMin = 0f;
		public const float ofnlMax = 1f;

		private const float fnlRangeSize = fnlMax - fnlMin;


		// FastNoiseLite returns in range [-1,1]
		// so we scale the value to be in range [0,1] here for lerp / height map calls.
		private static float ScaleNoiseValue(float v) => Mathf.Clamp01((v + fnlMax) / fnlRangeSize);

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

		public static float At(this FastNoiseLite noise, float x, float y)
		{
			return GetLerpDomainWarpNoise(noise, x, y);
		}

		public static float At(this FastNoiseLite noise, float x, float y, float z)
		{
			return GetLerpDomainWarpNoise(noise, x, y, z);
		}

		public static float At(this FastNoiseLite noise, float x, float y, bool domainWarp)
		{
			return domainWarp ? GetLerpDomainWarpNoise(noise, x, y) : GetLerpNoise(noise, x, y);
		}

		public static float At(this FastNoiseLite noise, float x, float y, float z, bool domainWarp)
		{
			return domainWarp ? GetLerpDomainWarpNoise(noise, x, y, z) : GetLerpNoise(noise, x, y, z);
		}

		public static void SetOptions(this FastNoiseLite noise, FastNoiseOptions options)
		{
			if (noise == null || options == null) return;

			noise.SetFrequency(options.Frequency);
			noise.SetFractalType(options.FractalType);
			noise.SetFractalLacunarity(options.Lacunarity);
			noise.SetFractalWeightedStrength(options.WeightedStrength);
			noise.SetFractalPingPongStrength(options.PingPongStrength);
			noise.SetCellularDistanceFunction(options.CellularDistanceFunction);
			noise.SetCellularReturnType(options.CellularReturnType);
			noise.SetCellularJitter(options.CellularJitterModifier);
			noise.SetDomainWarpAmp(options.DomainWarpAmp);
			noise.SetNoiseType(options.NoiseType);
			noise.SetFractalOctaves(options.Octaves);
			noise.SetFractalGain(options.Gain);
			noise.SetDomainWarpType(options.DomainWarpType);
			noise.SetRotationType3D(options.RotationType3D);
		}
	}
}
