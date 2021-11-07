namespace Obsidize.FastNoise.EditorTools
{
	public abstract class FastNoiseModuleEditorBase : FastNoisePreviewEditorBase
	{

		public abstract FastNoiseModule Module { get; }
		public override FastNoisePreviewOptions FastNoisePreview => Module?.Preview;

		public override void UpdatePreviewTexture()
		{
			Module?.DrawPreview(FastNoisePreviewTexture);
		}

		public override void OnInspectorGUI()
		{
			bool didUpdate = DrawDefaultInspector();
			DrawFastNoisePreviewOptions(didUpdate);
		}
	}
}
