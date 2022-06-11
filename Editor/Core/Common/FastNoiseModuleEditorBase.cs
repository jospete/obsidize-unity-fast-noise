namespace Obsidize.FastNoise.Editor
{
	public abstract class FastNoiseModuleEditorBase : FastNoisePreviewEditorBase
	{

		private FastNoiseContext _cachedContext;

		public abstract FastNoiseModule Module { get; }
		public override FastNoisePreviewOptions FastNoisePreview => Module?.Preview;

		public void RegenerateContext()
		{
			_cachedContext = Module?.CreateContext() ?? null;
		}

		public override void UpdatePreviewTexture()
		{
			FastNoisePreview?.DrawPreviewTexture(_cachedContext, FastNoisePreviewTexture);
		}

		public virtual bool DrawDefaultNoiseModuleInspector()
		{

			if (_cachedContext == null) RegenerateContext();

			bool didUpdate = DrawDefaultInspector();
			if (didUpdate) RegenerateContext();

			return didUpdate;
		}

		public override void OnInspectorGUI()
		{
			bool didUpdate = DrawDefaultNoiseModuleInspector();
			DrawFastNoisePreviewOptions(didUpdate);
		}
	}
}
