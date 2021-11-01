using UnityEngine;

namespace Obsidize.FastNoise
{

	using NoiseType = FastNoiseLite.NoiseType;
	using RotationType3D = FastNoiseLite.RotationType3D;
	using FractalType = FastNoiseLite.FractalType;
	using CellularDistanceFunction = FastNoiseLite.CellularDistanceFunction;
	using CellularReturnType = FastNoiseLite.CellularReturnType;
	using DomainWarpType = FastNoiseLite.DomainWarpType;

	[CreateAssetMenu(menuName = "Fast Noise/Module", fileName = "FastNoiseModule")]
	public class FastNoiseOptions : FastNoisePipelineModule
	{

		private const int octavesMin = 1;
		private const int octavesMax = 12;

		protected readonly FastNoiseLite noise = new FastNoiseLite();

		public FastNoisePreviewOptions preview = new FastNoisePreviewOptions();

		[Header("General")]
		[SerializeField] private NoiseType _noiseType = NoiseType.OpenSimplex2;
		[SerializeField] private RotationType3D _rotationType3D = RotationType3D.None;
		[SerializeField] private float _frequency = 0.01f;

		[Header("Fractals")]
		[SerializeField] private FractalType _fractalType = FractalType.FBm;
		[SerializeField] private int _octaves = 4;
		[SerializeField] private float _lacunarity = 2.0f;
		[SerializeField] private float _gain = 0.5f;
		[SerializeField] private float _weightedStrength = 0.15f;
		[SerializeField] private float _pingPongStength = 0f;

		[Header("Cellular")]
		[SerializeField] private CellularDistanceFunction _cellularDistanceFunction = CellularDistanceFunction.EuclideanSq;
		[SerializeField] private CellularReturnType _cellularReturnType = CellularReturnType.Distance;
		[SerializeField] private float _cellularJitterModifier = 0f;

		[Header("Domain Warp")]
		[SerializeField] private DomainWarpType _domainWarpType = DomainWarpType.BasicGrid;
		[SerializeField] private float _domainWarpAmp = 0f;

		public float Frequency
		{
			get => _frequency;
			set => _frequency = Mathf.Max(0.001f, value);
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
			set => _lacunarity = Mathf.Max(0f, value);
		}

		public float Gain
		{
			get => _gain;
			set => _gain = Mathf.Max(0f, value);
		}

		public float WeightedStrength
		{
			get => _weightedStrength;
			set => _weightedStrength = value;
		}

		public float PingPongStrength
		{
			get => _pingPongStength;
			set => _pingPongStength = value;
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

		private void Awake()
		{
			noise.SetOptions(this);
		}

		private void OnValidate()
		{
			Frequency = Frequency;
			Octaves = Octaves;
			Lacunarity = Lacunarity;
			Gain = Gain;
			if (preview != null) preview.Validate();
		}

		public override void SetSeed(int seed)
		{
			noise.SetOptions(this);
			noise.SetSeed(seed);
		}

		public override float GetNoise(float x, float y)
		{
			return noise.At(x, y);
		}

		public override float GetNoise(float x, float y, float z)
		{
			return noise.At(x, y, z);
		}

		public void DrawPreview(Texture2D texture)
		{
			if (noise == null || preview == null) return;
			noise.SetOptions(this);
			preview.DrawPreviewTexture(this, texture);
		}
	}
}
