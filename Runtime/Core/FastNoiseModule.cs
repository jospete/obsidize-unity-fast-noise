using UnityEngine;

namespace Obsidize.FastNoise
{
	public abstract class FastNoiseModule : ScriptableObject
	{

		[SerializeField] private FastNoisePreviewOptions _preview = new FastNoisePreviewOptions();

		public FastNoisePreviewOptions Preview => _preview;

		public abstract FastNoiseContext CreateContext();

		protected virtual void OnValidate()
		{
			Preview?.Validate();
		}
	}
}
