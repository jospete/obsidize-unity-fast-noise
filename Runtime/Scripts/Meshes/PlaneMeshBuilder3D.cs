using UnityEngine;

namespace Obsidize.FastNoise
{
	public class PlaneMeshBuilder3D : PlaneMeshBuilder
	{

		protected override Vector3 CreateSeedVertexPosition(int x, int y)
		{
			// For 3D, assume we are on the XZ plane
			return new Vector3(x, 0f, y);
		}

		public void SetVertexHeight(int vertexOffset, float height)
		{
			var previousPosition = GetVertexAt(vertexOffset);
			SetVertexAt(vertexOffset, new Vector3(previousPosition.x, height, previousPosition.z));
		}
	}
}
