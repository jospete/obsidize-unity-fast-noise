using UnityEngine;
using Obsidize.FastNoise;

[DisallowMultipleComponent]
public class Noise3DTerrainChunk : Noise3DTerrainChunkBase
{

	[SerializeField] private MeshFilter _noiseMeshFilter;
	[SerializeField] private MeshCollider _noiseMeshCollider;

	private readonly PlaneMeshBuilder3D meshBuilder = new PlaneMeshBuilder3D();

	private Mesh noiseMesh;
	private Color[] _colors;

	private void MoveToBoundsPosition(RectInt bounds)
	{
		var position = bounds.position;
		transform.localPosition = new Vector3(position.x, 0f, position.y);
	}

	private void EnsureMeshCreated()
	{
		if (noiseMesh == null)
		{
			noiseMesh = _noiseMeshCollider.sharedMesh = _noiseMeshFilter.mesh = new Mesh();
			noiseMesh.name = "Custom Mesh";
		}
	}

	private void ResizeToFitBounds(RectInt bounds)
	{

		meshBuilder.Resize(bounds.size);

		if (_colors == null || _colors.Length != meshBuilder.VertexCount)
		{
			System.Array.Resize(ref _colors, meshBuilder.VertexCount);
		}
	}

	private void SyncMeshData(RectInt bounds, FastNoiseContext noise, Noise3DTerrainChunkOptions options)
	{

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
	}

	public override void Generate(RectInt bounds, FastNoiseContext noise, Noise3DTerrainChunkOptions options)
	{
		Generate(bounds, noise, options, false);
	}

	public void Generate(RectInt bounds, FastNoiseContext noise, Noise3DTerrainChunkOptions options, bool reposition)
	{

		if (noise == null || options == null)
		{
			return;
		}

		if (reposition)
		{
			MoveToBoundsPosition(bounds);
		}

		EnsureMeshCreated();
		ResizeToFitBounds(bounds);
		SyncMeshData(bounds, noise, options);

		noiseMesh.Clear();
		meshBuilder.ApplyTo(noiseMesh);
		noiseMesh.colors = _colors;
	}
}
