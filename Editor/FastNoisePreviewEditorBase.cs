using UnityEngine;
using UnityEditor;

namespace Obsidize.FastNoise.EditorTools
{

	public abstract class FastNoisePreviewEditorBase : Editor
	{

		private const float minDragDamp = 0.25f;

		private Texture2D _previewTexture;
		private bool _textureRequiresInit;
		private bool _hasActiveDrag;
		private bool _manualSetDirty;

		public Texture2D FastNoisePreviewTexture => _previewTexture;
		public int FastNoisePreviewTextureHeight => _previewTexture != null ? _previewTexture.height : 0;

		public abstract FastNoisePreviewOptions FastNoisePreview { get; }

		public abstract void UpdatePreviewTexture();

		private void UpdateZoom(float delta)
		{

			if (FastNoisePreview == null) return;

			FastNoisePreview.Zoom += delta;
			UpdatePreviewTexture();
		}

		private void UpdateConfigOffset(Vector2 delta)
		{

			if (FastNoisePreview == null) return;

			FastNoisePreview.offset += new Vector2Int(
				-Mathf.RoundToInt(delta.x),
				Mathf.RoundToInt(delta.y)
			);

			UpdatePreviewTexture();
		}

		private void CheckForPreviewDrag(Rect previewArea)
		{

			var preview = FastNoisePreview;
			if (preview == null) return;

			var e = Event.current;
			var type = e.type;
			var isMouseInPreviewArea = previewArea.Contains(e.mousePosition);

			if (type == EventType.ScrollWheel && isMouseInPreviewArea)
			{
				UpdateZoom(e.delta.y * -0.025f);
				e.Use();
				return;
			}

			if (type == EventType.MouseDown && !_hasActiveDrag && isMouseInPreviewArea)
			{
				_hasActiveDrag = true;
				e.Use();
				return;
			}

			if (type == EventType.MouseUp && _hasActiveDrag)
			{
				_hasActiveDrag = false;
				e.Use();
				return;
			}

			if (type == EventType.MouseDrag && _hasActiveDrag)
			{

				// Account for the zoom by artificially increasing the texture size
				var textureHeight = FastNoisePreviewTextureHeight * preview.RoundedZoomStep;

				// As the size of the preview grows, the drag event should shrink
				var damp = Mathf.Max(minDragDamp, (textureHeight / previewArea.height));
				UpdateConfigOffset(e.delta * damp);
				e.Use();
			}
		}

		public override bool HasPreviewGUI()
		{
			return true;
		}

		public override void OnInteractivePreviewGUI(Rect r, GUIStyle background)
		{
			if (_previewTexture == null) return;

			CheckForPreviewDrag(r);
			EditorGUI.DrawPreviewTexture(r, _previewTexture, null, ScaleMode.ScaleToFit);
		}

		public void DrawFastNoisePreviewOptions(bool forceTextureUpdate)
		{

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.BeginHorizontal();

			if (GUILayout.Button("Reset Offset"))
			{
				FastNoisePreview.ResetOffset();
				_manualSetDirty = true;
			}

			if (GUILayout.Button("Invert Color Range"))
			{
				FastNoisePreview.SwapColorRange();
				_manualSetDirty = true;
			}

			EditorGUILayout.EndHorizontal();

			var previewHeight = FastNoisePreviewTextureHeight;

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Preview Aspect: " + previewHeight + "x" + previewHeight);

			_textureRequiresInit = !FastNoisePreview.ValidateTextureDimensions(ref _previewTexture);

			if (forceTextureUpdate || _textureRequiresInit || _manualSetDirty)
			{
				UpdatePreviewTexture();
			}

			if (_manualSetDirty)
			{
				EditorUtility.SetDirty(target);
			}
		}
	}
}
