using UnityEngine;

namespace Obsidize.FastNoise
{
	public class PlaneMeshBuilder2D : PlaneMeshBuilder
	{
		protected override Vector3 CreateSeedVertexPosition(int x, int y)
		{
			// Assume we are on the XY plane for 2D builders
			return new Vector3(x, y, 0f);
		}
	}
}
