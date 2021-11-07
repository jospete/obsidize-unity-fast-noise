using UnityEngine;

namespace Obsidize.FastNoise
{
	public abstract class FastNoiseModule : ScriptableObject
	{

		[SerializeField] private FastNoisePreviewOptions _preview = new FastNoisePreviewOptions();

		public FastNoisePreviewOptions Preview => _preview;

		public abstract void SetSeed(int seed);
		public abstract float GetNoise(float x, float y);
		public abstract float GetNoise(float x, float y, float z);

		public float GetNoise(Vector2 position)
		{
			return GetNoise(position.x, position.y);
		}

		public float GetNoise(Vector2Int position)
		{
			return GetNoise(position.x, position.y);
		}

		public float GetNoise(Vector3 position)
		{
			return GetNoise(position.x, position.y, position.z);
		}

		public float GetNoise(Vector3Int position)
		{
			return GetNoise(position.x, position.y, position.z);
		}

		public virtual void DrawPreview(Texture2D texture)
		{
			Preview?.DrawPreviewTexture(this, texture);
		}

		public virtual void Validate()
		{
			Preview?.Validate();
		}

		private void OnValidate()
		{
			Validate();
		}
	}
}
