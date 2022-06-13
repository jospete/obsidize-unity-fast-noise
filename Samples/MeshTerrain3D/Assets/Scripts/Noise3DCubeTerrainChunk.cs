using Obsidize.FastNoise;
using UnityEngine;

[DisallowMultipleComponent]
public class Noise3DCubeTerrainChunk : Noise3DTerrainChunkBase
{

	[SerializeField] private MeshRenderer _cubePrefab;

	[Header("Debug Info")]
	private MeshRenderer[] _cubes;

	private void Resize(int cellCount)
	{

		var safeCellCount = Mathf.Max(cellCount, 1);

		if (_cubes == null || _cubes.Length != safeCellCount)
		{
			System.Array.Resize(ref _cubes, safeCellCount);
		}
	}

	private MeshRenderer EnsureCubeAt(int offset)
	{

		var result = _cubes[offset];

		if (result == null)
		{
			result = _cubes[offset] = Instantiate(_cubePrefab, transform);
		}

		return result;
	}

	private void InitializeCubeAt(int offset, int x, int y, float height, Color color)
	{
		var cube = EnsureCubeAt(offset);
		cube.transform.localPosition = new Vector3(x, height / 2f, y);
		cube.transform.localScale = new Vector3(1f, height, 1f);
		cube.material.color = color;
	}

	public override void Generate(RectInt bounds, FastNoiseContext noise, Noise3DTerrainChunkOptions options)
	{

		int offset = 0;
		var maxHeight = options.MaxHeight;
		var heightGradient = options.HeightMapGradient;

		Resize(bounds.width * bounds.height);

		for (int y = 0, noiseY = bounds.yMin; y < bounds.height; y++, noiseY++)
		{
			for (int x = 0, noiseX = bounds.xMin; x < bounds.width; x++, noiseX++)
			{

				var noiseValue = noise.GetNoise(noiseX, noiseY);
				var cellHeight = noiseValue * maxHeight;
				var cellColor = heightGradient.Evaluate(noiseValue);

				InitializeCubeAt(offset, x, y, cellHeight, cellColor);

				offset++;
			}
		}
	}
}
