using UnityEditor;

namespace Obsidize.FastNoise.EditorTools
{

	[CustomEditor(typeof(FastNoisePreset))]
	public class FastNoisePresetEditor : FastNoiseModuleEditorBase
	{

		private FastNoisePreset _config;

		public override FastNoiseModule Module => _config;

		public override void OnInspectorGUI()
		{
			_config = target as FastNoisePreset;
			base.OnInspectorGUI();
		}
	}
}
