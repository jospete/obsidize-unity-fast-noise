using UnityEditor;

namespace Obsidize.FastNoise.Editor
{

	[CustomEditor(typeof(FastNoiseSuppressor))]
	public class FaseNoiseSuppressorEditor : FastNoiseModuleEditorBase
	{

		private FastNoiseSuppressor _module;

		public override FastNoiseModule Module => _module;

		public override void OnInspectorGUI()
		{
			_module = target as FastNoiseSuppressor;
			base.OnInspectorGUI();
		}
	}
}

