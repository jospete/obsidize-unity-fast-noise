using UnityEngine;
using Obsidize.FastNoise;

[DisallowMultipleComponent]
public class Noise3DTerrainGenerator : MonoBehaviour
{

	[Header("Configuration")]
	[SerializeField] private Noise3DTerrainChunkOptions _options;

	// NOTE:
	// The seed and offset fields can be changed during runtime
	// to dynamically update the chunk mesh.
	[SerializeField] private int _seed;
	[SerializeField] private Vector2Int _offset;

	private FastNoiseContext _noise;
	private Noise3DTerrainChunk _chunk;
	private Vector2Int _generatedOffset;
	private int _generatedSeed;

	private void Start()
	{
		_noise = _options.NoiseModule.CreateContext();
		_chunk = Instantiate(_options.ChunkPrefab);

		_chunk.transform.parent = transform;
		_chunk.transform.position = Vector3.zero;

		RegenerateChunk();
	}

	private void Update()
	{
		if (_offset != _generatedOffset || _seed != _generatedSeed)
		{
			RegenerateChunk();
		}
	}

	private void RegenerateChunk()
	{
		var chunkBounds = new RectInt(_offset, _options.ChunkSize);

		_noise.SetSeed(_seed);
		_chunk.Generate(chunkBounds, _noise, _options);

		_generatedSeed = _seed;
		_generatedOffset = _offset;
	}
}
