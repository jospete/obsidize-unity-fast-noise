using UnityEngine;
using Obsidize.FastNoise;

[DisallowMultipleComponent]
public class Noise3DTerrainChunk : MonoBehaviour
{

	[SerializeField] private MeshFilter _noiseMeshFilter;
	[SerializeField] private MeshCollider _noiseMeshCollider;

	private readonly PlaneMeshBuilder meshBuilder = new PlaneMeshBuilder();

	private Mesh noiseMesh;
	private Color[] _colors;

	public void Generate(RectInt bounds, FastNoiseContext noise, Noise3DTerrainChunkOptions options, bool reposition = false)
	{

		if (noise == null || options == null)
		{
			return;
		}

		if (noiseMesh == null)
		{
			noiseMesh = _noiseMeshCollider.sharedMesh = _noiseMeshFilter.mesh = new Mesh();
			noiseMesh.name = "Custom Mesh";
		}

		var position = bounds.position;

		if (reposition)
		{
			transform.localPosition = new Vector3(position.x, 0f, position.y);
		}

		meshBuilder.Resize(bounds.size);

		if (_colors == null || _colors.Length != meshBuilder.VertexCount)
		{
			System.Array.Resize(ref _colors, meshBuilder.VertexCount);
		}

		int offset = 0;
		var maxHeight = options.MaxHeight;
		var heightGradient = options.HeightMapGradient;

		for (int vertY = 0, noiseY = bounds.yMin; vertY < meshBuilder.VertexHeight; vertY++, noiseY++)
		{
			for (int vertX = 0, noiseX = bounds.xMin; vertX < meshBuilder.VertexHeight; vertX++, noiseX++)
			{

				var noiseValue = noise.GetNoise(noiseX, noiseY);
				var vertexHeight = noiseValue * maxHeight;

				meshBuilder.SetVertexHeight(offset, vertexHeight);
				_colors[offset] = heightGradient.Evaluate(noiseValue);

				offset++;
			}
		}

		noiseMesh.Clear();
		meshBuilder.ApplyTo(noiseMesh);
		noiseMesh.colors = _colors;
	}
}
