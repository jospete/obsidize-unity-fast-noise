using UnityEditor;

namespace Obsidize.FastNoise.EditorTools
{

	[CustomEditor(typeof(FastNoiseAmplifier))]
	public class FastNoiseAmplifierEditor : FastNoiseModuleEditorBase
	{

		private FastNoiseAmplifier _amplifier;

		public override FastNoiseModule Module => _amplifier;

		public override void OnInspectorGUI()
		{
			_amplifier = target as FastNoiseAmplifier;
			base.OnInspectorGUI();
		}
	}
}
