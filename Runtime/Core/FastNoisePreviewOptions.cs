using UnityEngine;

namespace Obsidize.FastNoise
{
	[System.Serializable]
	public class FastNoisePreviewOptions
	{

		private const int densityMin = 1;
		private const int densityMax = 8;
		private const float zoomMin = 0.01f;
		private const float zoomMax = 1f;

		public int seed = 1337;
		public Color lowColor = Color.black;
		public Color highColor = Color.white;
		public Vector2Int offset = Vector2Int.zero;

		[Range(densityMin, densityMax)] [SerializeField] private int _density = 2;
		[Range(zoomMin, zoomMax)] [SerializeField] private float _zoom = 0.5f;

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

		public int PreviewAspect => Density * 64;
		public float ZoomStep => Mathf.Max(1f, 1 / Mathf.Max(zoomMin, Zoom));
		public int RoundedZoomStep => Mathf.RoundToInt(ZoomStep);

		private Color32[] _colorBuffer;

		public Color GetRangeColor(float t)
		{
			return Color.Lerp(lowColor, highColor, t);
		}

		public void Validate()
		{
			Density = Density;
			Zoom = Zoom;
		}

		public void ResetOffset()
		{
			offset = Vector2Int.zero;
		}

		public void SwapColorRange()
		{
			var tmp = highColor;
			highColor = lowColor;
			lowColor = tmp;
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

			noise.SetSeed(seed);

			var colors = GetPreviewTextureColors(noise, texture.width, texture.height);

			if (colors == null) return false;

			texture.SetPixels32(colors, 0);
			texture.Apply();

			return true;
		}

		protected Color32[] GetPreviewTextureColors(IFastNoiseContext noise, int width, int height)
		{

			if (noise == null) return null;

			var targetBufferSize = width * height;

			if (_colorBuffer == null || _colorBuffer.Length != targetBufferSize)
			{
				System.Array.Resize(ref _colorBuffer, targetBufferSize);
			}

			var step = RoundedZoomStep;
			var xMin = offset.x;
			var xMax = xMin + (width * step);
			var yMin = offset.y;
			var yMax = yMin + (height * step);
			var index = 0;

			for (int y = yMin; y < yMax; y += step)
			{
				for (int x = xMin; x < xMax; x += step)
				{
					_colorBuffer[index] = GetRangeColor(noise.GetNoise(x, y));
					index++;
				}
			}

			return _colorBuffer;
		}
	}
}
