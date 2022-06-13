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
	[SerializeField] private float _offsetChangeSpeed = 5f;
	[SerializeField] private string _inputSeed = "1337";
	[SerializeField] private Vector2Int _offset;

	private FastNoiseContext _noise;
	private Noise3DTerrainChunkBase _chunk;
	private Vector2Int _generatedOffset;
	private int _generatedSeed;
	private int _seed;

	public Vector2Int Offset
	{
		get => _offset;
		set
		{
			_offset = value;
		}
	}

	public string Seed
	{
		get => _inputSeed;
		set
		{
			_inputSeed = value;
			_seed = string.IsNullOrEmpty(_inputSeed) ? 0 : _inputSeed.GetHashCode();
		}
	}

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

		var yChange = Input.GetAxis("Horizontal") * _offsetChangeSpeed;
		var xChange = -Input.GetAxis("Vertical") * _offsetChangeSpeed;
		var offsetDelta = new Vector2Int(Mathf.RoundToInt(xChange), Mathf.RoundToInt(yChange));

		_offset += offsetDelta;

		Seed = _inputSeed;

		if (_offset != _generatedOffset || _seed != _generatedSeed)
		{
			RegenerateChunk();
		}
	}

	public void ResetOffset()
	{
		_offset = Vector2Int.zero;
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
