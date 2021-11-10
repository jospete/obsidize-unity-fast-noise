using UnityEngine;

namespace Obsidize.FastNoise
{
	[CreateAssetMenu(menuName = "Fast Noise/Preview/Preset", fileName = "FastNoisePreviewPreset")]
	public class FastNoisePreviewPreset : ScriptableObject
	{

		[SerializeField]
		private FastNoisePreviewOptions _options;

		public FastNoisePreviewOptions Options => _options;
	}
}
