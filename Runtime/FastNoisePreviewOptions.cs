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
		[Range(densityMin, densityMax)] public int density = 2;
		[Range(zoomMin, zoomMax)] public float zoom = 0.5f;

		public int PreviewAspect => density * 64;
		public float ZoomStep => Mathf.Max(1f, 1 / Mathf.Max(zoomMin, zoom));
		public int RoundedZoomStep => Mathf.RoundToInt(ZoomStep);

		private Color32[] _colorBuffer;

		public Color GetRangeColor(float t)
		{
			return Color.Lerp(lowColor, highColor, t);
		}

		public void Validate()
		{
			density = Mathf.Clamp(density, densityMin, densityMax);
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

			if (texture != null && texture.height == targetAspect)
			{
				return true;
			}

			texture = new Texture2D(targetAspect, targetAspect);
			return false;
		}

		public void DrawPreviewTexture(FastNoisePipelineModule noise, Texture2D texture)
		{
			if (noise == null || texture == null) return;

			noise.SetSeed(seed);

			var colors = GetPreviewTextureColors(noise, texture.width, texture.height);

			if (colors == null) return;

			texture.SetPixels32(colors, 0);
			texture.Apply();
		}

		protected Color32[] GetPreviewTextureColors(FastNoisePipelineModule noise, int width, int height)
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
