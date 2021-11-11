using System;
using UnityEngine;

public class PlaneMeshBuilder
{

	private const int trianglesPerSquare = 2;
	private const int verticesPerTriangle = 3;
	private const int triangleVertsPerSquare = trianglesPerSquare * verticesPerTriangle;

	private Vector3[] _verticies;
	private int[] _triangles;

	private int _width;
	private int _height;
	private int _vertexWidth;
	private int _vertexHeight;
	private int _cellCount;
	private int _vertexCount;
	private int _triangleCount;

	public int Width => _width;
	public int VertexWidth => _vertexWidth;
	public int Height => _height;
	public int VertexHeight => _vertexHeight;
	public int VertexCount => _vertexCount;

	public void Resize(Vector2Int size)
	{
		Resize(size.x, size.y);
	}

	public void ApplyTo(Mesh mesh)
	{
		if (mesh == null) return;

		mesh.vertices = _verticies;
		mesh.triangles = _triangles;
	}

	public void SetVertexHeight(int vertexOffset, float height)
	{
		var previousPosition = _verticies[vertexOffset];
		_verticies[vertexOffset] = new Vector3(previousPosition.x, height, previousPosition.z);
	}

	private void InitializeVertexAt(int vertexOffset, int x, int y)
	{
		_verticies[vertexOffset] = new Vector3(x, 0f, y);
	}

	private int InitializeTrianglesAt(int trianglesOffset, int topRightVertexIndex)
	{

		var offset = trianglesOffset;
		var topRight = topRightVertexIndex;
		var topLeft = topRight - 1;
		var bottomRight = topRight - _vertexWidth;
		var bottomLeft = bottomRight - 1;

		_triangles[offset++] = bottomLeft;
		_triangles[offset++] = topLeft;
		_triangles[offset++] = bottomRight;

		_triangles[offset++] = bottomRight;
		_triangles[offset++] = topLeft;
		_triangles[offset++] = topRight;

		return offset;
	}

	// Pre-bakes the verticies and triangles, so that
	// heights can be subsequently set as fast as possible.
	public void Resize(int width, int height)
	{

		var newWidth = Mathf.Max(1, width);
		var newHeight = Mathf.Max(1, height);

		if (newWidth == _width && newHeight == _height)
		{
			return;
		}

		_width = newWidth;
		_height = newHeight;
		_vertexWidth = _width + 1;
		_vertexHeight = _height + 1;
		_cellCount = _width * _height;
		_vertexCount = _vertexWidth * _vertexHeight;
		_triangleCount = _cellCount * triangleVertsPerSquare;

		if (
			_verticies != null
			&& _verticies.Length == _vertexCount
			&& _triangles != null
			&& _triangles.Length == _triangleCount
		)
		{
			return;
		}

		Array.Resize(ref _verticies, _vertexCount);
		Array.Resize(ref _triangles, _triangleCount);

		int x, y, vertexOffset, trianglesOffset;

		// Insert bottom edge verticies
		for (x = 0; x < _vertexWidth; x++)
		{
			InitializeVertexAt(x, x, 0);
		}

		// Insert left edge verticies (we can skip the first one since it was already made in the previous loop)
		for (y = 1, vertexOffset = _vertexWidth; y < _vertexHeight; y++, vertexOffset += _vertexWidth)
		{
			InitializeVertexAt(vertexOffset, 0, y);
		}

		// Start at position [1, 1] so we can stitch together triangles by looking backwards
		vertexOffset = _vertexWidth + 1;
		trianglesOffset = 0;

		// Insert all remaining verticies, and construct triangle indicies
		for (y = 1; y < _vertexHeight; y++)
		{

			for (x = 1; x < _vertexWidth; x++)
			{
				InitializeVertexAt(vertexOffset, x, y);
				trianglesOffset = InitializeTrianglesAt(trianglesOffset, vertexOffset);
				vertexOffset++;
			}

			// Increment an additional time to skip the first column since
			// we already made it in the first loop
			vertexOffset++;
		}
	}
}
