using Obsidize.FastNoise;
using UnityEngine;

public abstract class Noise3DTerrainChunkBase : MonoBehaviour
{

	public abstract void Generate(
		RectInt bounds,
		FastNoiseContext noise,
		Noise3DTerrainChunkOptions options
	);
}
