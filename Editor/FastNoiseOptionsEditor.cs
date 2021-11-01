using UnityEditor;

namespace Obsidize.FastNoise
{

	[CustomEditor(typeof(FastNoiseOptions))]
	public class FastNoiseOptionsEditor : FastNoisePreviewEditorBase
	{

		private FastNoiseOptions _config;
		private bool _didUpdateConfig;

		public override FastNoisePreviewOptions FastNoisePreview => _config?.preview;

		public override void UpdatePreviewTexture()
		{
			_config.DrawPreview(FastNoisePreviewTexture);
		}

		public override void OnInspectorGUI()
		{
			_didUpdateConfig = DrawDefaultInspector();
			_config = target as FastNoiseOptions;
			DrawFastNoisePreviewOptions(_didUpdateConfig);
		}
	}
}
