using UnityEngine;

namespace Obsidize.FastNoise
{
	[System.Serializable]
	public class FastNoisePreviewOptions
	{

		public enum NoisePreviewFormat : ushort
		{
			Noise2D = 0,
			Noise1D = 1
		}

		private const int densityMin = 1;
		private const int densityMax = 8;
		private const int densityDefault = 4;
		private const int densityStep = 32;

		private const float zoomMin = 0.01f;
		private const float zoomMax = 1f;
		private const float zoomDefault = 0.5f;

		[Header("General")]
		[SerializeField] private int _seed = 1337;
		[SerializeField] private NoisePreviewFormat _format = NoisePreviewFormat.Noise2D;

		[Header("Noise Colors")]
		[SerializeField] private Color _lowColor = Color.black;
		[SerializeField] private Color _highColor = Color.white;

		[Header("Noise Range")]
		[Range(densityMin, densityMax)] [SerializeField] private int _density = densityDefault;
		[Range(zoomMin, zoomMax)] [SerializeField] private float _zoom = zoomDefault;
		[SerializeField] private Vector2Int _offset = Vector2Int.zero;

		private Color32[] _colorBuffer;

		public int PreviewAspect => Density * densityStep;
		public float ZoomStep => Mathf.Max(1f, 1 / Mathf.Max(zoomMin, Zoom));
		public int RoundedZoomStep => Mathf.RoundToInt(ZoomStep);
		public Vector2Int TextureDimensions => Vector2Int.one * PreviewAspect;

		public NoisePreviewFormat Format
		{
			get => _format;
			set => _format = value;
		}

		public int Seed
		{
			get => _seed;
			set => _seed = value;
		}

		public Color LowColor
		{
			get => _lowColor;
			set => _lowColor = value;
		}

		public Color HighColor
		{
			get => _highColor;
			set => _highColor = value;
		}

		public int Density
		{
			get => _density;
			set => _density = Mathf.Clamp(value, densityMin, densityMax);
		}

		public float Zoom
		{
			get => _zoom;
			set => _zoom = Mathf.Clamp(value, zoomMin, zoomMax);
		}

		public Vector2Int Offset
		{
			get => _offset;
			set => UpdateOffset(value);
		}

		public void ResetZoom()
		{
			Zoom = zoomDefault;
		}

		public void ResetDensity()
		{
			Density = densityDefault;
		}

		public void ResetOffset()
		{
			Offset = Vector2Int.zero;
		}

		public Color GetRangeColor(float t)
		{
			return Color.Lerp(LowColor, HighColor, t);
		}

		public void Validate()
		{
			Density = Density;
			Zoom = Zoom;
		}

		public void UpdateOffset(Vector2Int update)
		{
			_offset.x = update.x;

			if (Format != NoisePreviewFormat.Noise1D)
			{
				_offset.y = update.y;
			}
		}

		public void SwapColorRange()
		{
			var tmp = HighColor;
			HighColor = LowColor;
			LowColor = tmp;
		}

		public FastNoisePreviewOptions Clone()
		{
			var result = new FastNoisePreviewOptions();
			result.Configure(this);
			return result;
		}

		public void Configure(FastNoisePreviewOptions options)
		{

			if (options == null) return;

			Format = options.Format;
			Seed = options.Seed;
			LowColor = options.LowColor;
			HighColor = options.HighColor;
			Density = options.Density;
			Zoom = options.Zoom;
			Offset = options.Offset;

			Validate();
		}

		public bool ValidateTextureDimensions(ref Texture2D texture)
		{

			var targetAspect = PreviewAspect;

			if (
				texture != null
				&& texture.width == targetAspect
				&& texture.height == targetAspect
			)
			{
				return true;
			}

			texture = new Texture2D(targetAspect, targetAspect);
			return false;
		}

		public bool DrawPreviewTexture(IFastNoiseContext noise, Texture2D texture)
		{
			if (noise == null || texture == null) return false;

			noise.SetSeed(Seed);

			var colors = GetPreviewTextureColors(noise, texture.width, texture.height);

			if (colors == null) return false;

			texture.SetPixels32(colors, 0);
			texture.Apply();

			return true;
		}

		public bool DrawPreviewHeightMap(IFastNoiseContext noise, PlaneMeshBuilder3D meshBuilder, float heightAmplifier = 1f)
		{

			if (noise == null || meshBuilder == null) return false;

			var offset = Offset;
			var step = RoundedZoomStep;
			var xMin = offset.x;
			var xRange = meshBuilder.VertexWidth * step;
			var xNoiseMax = xMin + xRange;
			var yMin = offset.y;
			var yRange = meshBuilder.VertexHeight * step;
			var yNoiseMax = yMin + yRange;
			var index = 0;

			for (int yNoise = yMin; yNoise < yNoiseMax; yNoise += step)
			{
				for (int xNoise = xMin; xNoise < xNoiseMax; xNoise += step)
				{
					meshBuilder.SetVertexHeight(index++, noise.GetNoise(xNoise, yNoise) * heightAmplifier);
				}
			}

			return true;
		}

		protected Color32[] GetPreviewTextureColors(IFastNoiseContext noise, int width, int height)
		{
			return Format switch
			{
				NoisePreviewFormat.Noise1D => GetPreviewTextureColors1D(noise, width, height),
				_ => GetPreviewTextureColors2D(noise, width, height)
			};
		}

		protected Color32[] NormalizeColorsBuffer(IFastNoiseContext noise, int width, int height)
		{

			if (noise == null) return null;

			var targetBufferSize = width * height;

			if (_colorBuffer == null || _colorBuffer.Length != targetBufferSize)
			{
				System.Array.Resize(ref _colorBuffer, targetBufferSize);
			}

			return _colorBuffer;
		}

		protected Color32[] GetPreviewTextureColors1D(IFastNoiseContext noise, int width, int height)
		{

			var buffer = NormalizeColorsBuffer(noise, width, height);
			if (buffer == null) return null;

			var offset = Offset;
			var step = RoundedZoomStep;
			float noiseValue;
			int noiseIndex;

			for (int x = 0; x < width; x++)
			{

				noiseValue = noise.GetNoise((x * step) + offset.x, offset.y);
				noiseIndex = Mathf.RoundToInt(noiseValue * height);
				noiseIndex = Mathf.Clamp(noiseIndex, 0, height - 1);

				for (int y = 0, index = x; y < height; y++, index += width)
				{
					buffer[index] = (y == noiseIndex) ? HighColor : LowColor;
				}
			}

			return buffer;
		}

		protected Color32[] GetPreviewTextureColors2D(IFastNoiseContext noise, int width, int height)
		{

			var buffer = NormalizeColorsBuffer(noise, width, height);
			if (buffer == null) return null;

			var offset = Offset;
			var step = RoundedZoomStep;
			var xMin = offset.x;
			var xRange = width * step;
			var xMax = xMin + xRange;
			var yMin = offset.y;
			var yRange = height * step;
			var yMax = yMin + yRange;
			var index = 0;

			for (int y = yMin; y < yMax; y += step)
			{
				for (int x = xMin; x < xMax; x += step)
				{
					buffer[index] = GetRangeColor(noise.GetNoise(x, y));
					index++;
				}
			}

			return buffer;
		}
	}
}
