using UnityEditor;

namespace Obsidize.FastNoise.EditorTools
{

	[CustomEditor(typeof(FastNoisePreset))]
	public class FastNoisePresetEditor : FastNoisePreviewEditorBase
	{

		private FastNoisePreset _config;
		private bool _didUpdateConfig;

		public override FastNoisePreviewOptions FastNoisePreview => _config?.Preview;

		public override void UpdatePreviewTexture()
		{
			_config.DrawPreview(FastNoisePreviewTexture);
		}

		public override void OnInspectorGUI()
		{
			_didUpdateConfig = DrawDefaultInspector();
			_config = target as FastNoisePreset;
			DrawFastNoisePreviewOptions(_didUpdateConfig);
		}
	}
}
