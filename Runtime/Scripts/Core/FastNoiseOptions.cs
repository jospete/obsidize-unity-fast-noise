using UnityEngine;

namespace Obsidize.FastNoise
{
	using NoiseType = FastNoiseLite.NoiseType;
	using RotationType3D = FastNoiseLite.RotationType3D;
	using FractalType = FastNoiseLite.FractalType;
	using CellularDistanceFunction = FastNoiseLite.CellularDistanceFunction;
	using CellularReturnType = FastNoiseLite.CellularReturnType;
	using DomainWarpType = FastNoiseLite.DomainWarpType;

	/// <summary>
	/// Core options exposed by the Auburn/FastNoiseLite implementation.
	/// This is designed to be a pure link-layer between the unity editor and
	/// the FastNoiseLite class, with as little intrusion on the core API as possible.
	///
	/// Other non-FastNoiseLite constructs added by the Obsidize.FastNoise package
	/// will be placed in a separate class / file, with functionality extensions
	/// based on the core FastNoiseLite implementation.
	/// </summary>
	[System.Serializable]
	public class FastNoiseOptions
	{

		private const float frequencyMin = 0.001f;
		private const float lacunarityMin = 0f;
		private const float gainMin = 0f;
		private const float pingPongStrengthMin = 0f;

		private const float weightedStrengthMin = 0f;
		private const float weightedStrengthMax = 1f;

		private const int octavesMin = 1;
		private const int octavesMax = 12;

		[Header("General")]

		[SerializeField]
		[Tooltip("Sets noise algorithm used for GetNoise(...)")]
		private NoiseType _noiseType = NoiseType.OpenSimplex2;

		[SerializeField]
		[Tooltip("Sets domain rotation type for 3D Noise and 3D DomainWarp. Can aid in reducing directional artifacts when sampling a 2D plane in 3D")]
		private RotationType3D _rotationType3D = RotationType3D.None;

		[SerializeField]
		[Tooltip("Sets frequency for all noise types")]
		private float _frequency = 0.01f;

		[Header("Fractals")]

		[SerializeField]
		[Tooltip("Sets method for combining octaves in all fractal noise types. Note: FractalType.DomainWarp... only affects DomainWarp(...)")]
		private FractalType _fractalType = FractalType.FBm;

		[SerializeField]
		[Tooltip("Sets octave count for all fractal noise types")]
		private int _octaves = 4;

		[SerializeField]
		[Tooltip("Sets octave lacunarity for all fractal noise types")]
		private float _lacunarity = 2.0f;

		[SerializeField]
		[Tooltip("Sets octave gain for all fractal noise types")]
		private float _gain = 0.5f;

		[SerializeField]
		[Tooltip("Sets octave weighting for all none DomainWarp fratal types")]
		[Range(weightedStrengthMin, weightedStrengthMax)]
		private float _weightedStrength = 0.15f;

		[SerializeField]
		[Tooltip("Sets strength of the fractal ping pong effect")]
		private float _pingPongStength = 2f;

		[Header("Cellular")]

		[SerializeField]
		[Tooltip("Sets distance function used in cellular noise calculations")]
		private CellularDistanceFunction _cellularDistanceFunction = CellularDistanceFunction.EuclideanSq;

		[SerializeField]
		[Tooltip("Sets return type from cellular noise calculations")]
		private CellularReturnType _cellularReturnType = CellularReturnType.Distance;

		[SerializeField]
		[Tooltip("Sets the maximum distance a cellular point can move from it's grid position. Note: Setting this higher than 1 will cause artifacts")]
		private float _cellularJitterModifier = 1f;

		[Header("Domain Warp")]

		[SerializeField]
		[Tooltip("Toggles usage of DomainWarp(...) in GetNoise(...) calculations")]
		private bool _useDomainWarp = true;

		[SerializeField]
		[Tooltip("Sets the warp algorithm when using DomainWarp(...)")]
		private DomainWarpType _domainWarpType = DomainWarpType.OpenSimplex2;

		[SerializeField]
		[Tooltip("Sets the maximum warp distance from original position when using DomainWarp(...)")]
		private float _domainWarpAmp = 1f;

		public float Frequency
		{
			get => _frequency;
			set => _frequency = Mathf.Max(value, frequencyMin);
		}

		public NoiseType NoiseType
		{
			get => _noiseType;
			set => _noiseType = value;
		}

		public RotationType3D RotationType3D
		{
			get => _rotationType3D;
			set => _rotationType3D = value;
		}

		public FractalType FractalType
		{
			get => _fractalType;
			set => _fractalType = value;
		}

		public int Octaves
		{
			get => _octaves;
			set => _octaves = Mathf.Clamp(value, octavesMin, octavesMax);
		}

		public float Lacunarity
		{
			get => _lacunarity;
			set => _lacunarity = Mathf.Max(value, lacunarityMin);
		}

		public float Gain
		{
			get => _gain;
			set => _gain = Mathf.Max(value, gainMin);
		}

		public float WeightedStrength
		{
			get => _weightedStrength;
			set => _weightedStrength = Mathf.Clamp(value, weightedStrengthMin, weightedStrengthMax);
		}

		public float PingPongStrength
		{
			get => _pingPongStength;
			set => _pingPongStength = Mathf.Max(value, pingPongStrengthMin);
		}

		public CellularDistanceFunction CellularDistanceFunction
		{
			get => _cellularDistanceFunction;
			set => _cellularDistanceFunction = value;
		}

		public CellularReturnType CellularReturnType
		{
			get => _cellularReturnType;
			set => _cellularReturnType = value;
		}

		public float CellularJitterModifier
		{
			get => _cellularJitterModifier;
			set => _cellularJitterModifier = value;
		}

		public bool UseDomainWarp
		{
			get => _useDomainWarp;
			set => _useDomainWarp = value;
		}

		public DomainWarpType DomainWarpType
		{
			get => _domainWarpType;
			set => _domainWarpType = value;
		}

		public float DomainWarpAmp
		{
			get => _domainWarpAmp;
			set => _domainWarpAmp = value;
		}

		public void Validate()
		{
			Frequency = Frequency;
			Octaves = Octaves;
			Lacunarity = Lacunarity;
			Gain = Gain;
			WeightedStrength = WeightedStrength;
			PingPongStrength = PingPongStrength;
		}
	}
}