namespace Obsidize.FastNoise.EditorTools
{
	public abstract class FastNoiseModuleEditorBase : FastNoisePreviewEditorBase
	{

		private FastNoiseContext _cachedContext;

		public abstract FastNoiseModule Module { get; }
		public override FastNoisePreviewOptions FastNoisePreview => Module?.Preview;

		public override void UpdatePreviewTexture()
		{
			if (_cachedContext == null) _cachedContext = Module.CreateContext();
			FastNoisePreview?.DrawPreviewTexture(_cachedContext, FastNoisePreviewTexture);
		}

		public override void OnInspectorGUI()
		{
			bool didUpdate = DrawDefaultInspector();
			DrawFastNoisePreviewOptions(didUpdate);
		}
	}
}
