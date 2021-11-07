using UnityEditor;

namespace Obsidize.FastNoise.EditorTools
{

	[CustomEditor(typeof(FastNoiseInverter))]
	public class FastNoiseInverterEditor : FastNoiseModuleEditorBase
	{

		private FastNoiseInverter _module;

		public override FastNoiseModule Module => _module;

		public override void OnInspectorGUI()
		{
			_module = target as FastNoiseInverter;
			base.OnInspectorGUI();
		}
	}
}
