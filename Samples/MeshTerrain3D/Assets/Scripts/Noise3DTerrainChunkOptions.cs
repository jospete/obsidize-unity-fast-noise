using UnityEngine;
using Obsidize.FastNoise;

[CreateAssetMenu(menuName = "Noise3D Terrain/Options", fileName = "Noise3DTerrainOptions")]
public class Noise3DTerrainChunkOptions : ScriptableObject
{

	[SerializeField] private FastNoiseModule _noiseModule;
	[SerializeField] private Gradient _heightMapGradient;
	[SerializeField] private Noise3DTerrainChunkBase _chunkPrefab;
	[SerializeField] private float _maxHeight = 50f;
	[SerializeField] private Vector2Int _chunkSize = Vector2Int.one;

	public FastNoiseModule NoiseModule => _noiseModule;
	public float MaxHeight => _maxHeight;
	public Gradient HeightMapGradient => _heightMapGradient;
	public Noise3DTerrainChunkBase ChunkPrefab => _chunkPrefab;
	public Vector2Int ChunkSize => _chunkSize;

	private void OnValidate()
	{
		_maxHeight = Mathf.Max(_maxHeight, 1f);
		_chunkSize.x = Mathf.Max(1, _chunkSize.x);
		_chunkSize.y = Mathf.Max(1, _chunkSize.y);
	}
}
