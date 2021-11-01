using UnityEngine;

namespace Obsidize.FastNoise
{
	[System.Serializable]
	public class FastNoisePipelineEntry
	{
		public FastNoisePipelineModule target;
		public float influence = 1f;

		public void SetSeed(int seed)
		{
			target?.SetSeed(seed);
		}

		public float GetInfluenceNoise(float maxInfluence, float x, float y)
		{
			if (target == null) return 0f;
			return target.GetNoise(x, y) * (influence / maxInfluence);
		}

		public float GetInfluenceNoise(float maxInfluence, float x, float y, float z)
		{
			if (target == null) return 0f;
			return target.GetNoise(x, y, z) * (influence / maxInfluence);
		}

		public bool ContainsModule(FastNoisePipelineModule module)
		{
			if (target == null) return false;
			if (target == module) return true;
			if (!(target is FastNoisePipeline)) return false;
			return (target as FastNoisePipeline).ContainsModule(module);
		}

		public void Validate(FastNoisePipeline parent = null)
		{

			influence = Mathf.Max(0f, influence);

			if (ContainsModule(parent))
			{
				Debug.LogWarning("Circular pipeline reference detected from parent " + parent + " to target " + target);
				target = null;
			}
		}
	}
}
