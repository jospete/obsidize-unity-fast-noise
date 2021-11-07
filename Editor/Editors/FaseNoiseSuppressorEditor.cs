using UnityEditor;

namespace Obsidize.FastNoise.EditorTools
{

	[CustomEditor(typeof(FastNoiseSuppressor))]
	public class FaseNoiseSuppressorEditor : FastNoiseModuleEditorBase
	{

		private FastNoiseSuppressor _suppressor;

		public override FastNoiseModule Module => _suppressor;

		public override void OnInspectorGUI()
		{
			_suppressor = target as FastNoiseSuppressor;
			base.OnInspectorGUI();
		}
	}
}

