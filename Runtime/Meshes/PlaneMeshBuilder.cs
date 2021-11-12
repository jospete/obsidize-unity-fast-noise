using System;
using UnityEngine;

namespace Obsidize.FastNoise
{
	/// <summary>
	/// Simple utility to pre-bake verticies and triangles for a plane mesh.
	/// 
	/// Here, a plane mesh is considered a set of verticies equally spaced out across a grid, 
	/// forming a set of linked quads across a single axis.
	/// 
	/// Note that the mesh can be used for both 3D height maps (on the XZ plane)
	/// as well as simple 2D tile maps (XY plane) - however, this does not account
	/// for UVs; it is left up to the user to determine the best method for that.
	/// </summary>
	public abstract class PlaneMeshBuilder
	{

		private const int widthMin = 1;
		private const int heightMin = 1;

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
		private int _triangleVertexCount;

		public int Width => _width;
		public int Height => _height;
		public int VertexWidth => _vertexWidth;
		public int VertexHeight => _vertexHeight;
		public int VertexCount => _vertexCount;
		public int CellCount => _cellCount;
		public int TriangleCount => _triangleCount;
		public int TriangleVertexCount => _triangleVertexCount;

		protected abstract Vector3 CreateSeedVertexPosition(int x, int y);

		protected Vector3 GetVertexAt(int index)
		{
			return _verticies[index];
		}

		protected void SetVertexAt(int index, Vector3 vertex)
		{
			_verticies[index] = vertex;
		}

		private void InitializeVertexAt(int vertexOffset, int x, int y)
		{
			SetVertexAt(vertexOffset, CreateSeedVertexPosition(x, y));
		}

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

		// Pre-bakes the verticies and triangles, so that
		// heights can be subsequently set as fast as possible.
		public void Resize(int width, int height)
		{

			var newWidth = Mathf.Max(widthMin, width);
			var newHeight = Mathf.Max(heightMin, height);

			if (newWidth == _width && newHeight == _height)
			{
				return;
			}

			_width = newWidth;
			_height = newHeight;

			BakeMeshData();
		}

		private int InitializeTrianglesAt(int trianglesOffset, int topRightVertexIndex)
		{

			var offset = trianglesOffset;
			var topRight = topRightVertexIndex;
			var topLeft = topRight - 1;
			var bottomRight = topRight - _vertexWidth;
			var bottomLeft = bottomRight - 1;

			// First triangle in the square -> |\
			_triangles[offset++] = bottomLeft;
			_triangles[offset++] = topLeft;
			_triangles[offset++] = bottomRight;

			// Second triangle in the square -> \|
			_triangles[offset++] = bottomRight;
			_triangles[offset++] = topLeft;
			_triangles[offset++] = topRight;

			return offset;
		}

		protected void BakeMeshData()
		{

			if (_width < widthMin || _height < heightMin)
			{
				Debug.LogError("Cannot bake mesh data without a valid size - use Resize() first");
				return;
			}

			_vertexWidth = _width + 1;
			_vertexHeight = _height + 1;
			_cellCount = _width * _height;
			_vertexCount = _vertexWidth * _vertexHeight;
			_triangleCount = _cellCount * trianglesPerSquare;
			_triangleVertexCount = _cellCount * triangleVertsPerSquare;

			if (_verticies == null || _verticies.Length != _vertexCount)
			{
				Array.Resize(ref _verticies, _vertexCount);
			}

			if (_triangles == null || _triangles.Length != _triangleVertexCount)
			{
				Array.Resize(ref _triangles, _triangleVertexCount);
			}

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
}
