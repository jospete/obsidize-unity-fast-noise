using UnityEditor;

namespace Obsidize.FastNoise.Editor
{

	[CustomEditor(typeof(FastNoisePreset))]
	public class FastNoisePresetEditor : FastNoiseModuleEditorBase
	{

		private FastNoisePreset _module;

		public override FastNoiseModule Module => _module;

		public override void OnInspectorGUI()
		{
			_module = target as FastNoisePreset;
			base.OnInspectorGUI();
		}
	}
}
