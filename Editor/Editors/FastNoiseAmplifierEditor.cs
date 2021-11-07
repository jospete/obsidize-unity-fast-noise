using UnityEditor;

namespace Obsidize.FastNoise.EditorTools
{

	[CustomEditor(typeof(FastNoiseAmplifier))]
	public class FastNoiseAmplifierEditor : FastNoiseModuleEditorBase
	{

		private FastNoiseAmplifier _module;

		public override FastNoiseModule Module => _module;

		public override void OnInspectorGUI()
		{
			_module = target as FastNoiseAmplifier;
			base.OnInspectorGUI();
		}
	}
}
