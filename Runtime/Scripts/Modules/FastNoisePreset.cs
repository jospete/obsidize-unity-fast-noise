using UnityEngine;

namespace Obsidize.FastNoise
{

	[CreateAssetMenu(menuName = "Fast Noise/Modules/Preset", fileName = "FastNoisePreset")]
	public class FastNoisePreset : FastNoiseModule
	{

		[Space]
		[SerializeField]
		private FastNoiseOptions _options = new FastNoiseOptions();

		public FastNoiseOptions Options => _options;

		public override FastNoiseContext CreateContext()
		{
			var result = new FastNoiseSourceContext();
			result.SetOptions(Options);
			return result;
		}
	}
}
