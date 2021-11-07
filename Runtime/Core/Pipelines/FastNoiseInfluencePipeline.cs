using System;
using UnityEngine;

namespace Obsidize.FastNoise
{

	[CreateAssetMenu(menuName = "Fast Noise/Pipelines/Influence", fileName = "FastNoiseInfluencePipeline")]
	public class FastNoiseInfluencePipeline : FastNoisePipeline
	{

		[HideInInspector] [SerializeField] private float[] _layerInfluences;
		[HideInInspector] [SerializeField] private float _totalInfluence;

		public float TotalInfluence => _totalInfluence;

		private void OnValidate()
		{
			Validate();
		}

		public float GetLayerInfluence(int layerIndex)
		{
			return _layerInfluences[layerIndex];
		}

		public float SetLayerInfluence(int layerIndex, float value)
		{
			var previousValue = GetLayerInfluence(layerIndex);
			var updateValue = _layerInfluences[layerIndex] = Mathf.Max(0f, value);
			if (previousValue != updateValue) CalculateTotalInfluence();
			return updateValue;
		}

		public float CalculateTotalInfluence()
		{
			var result = 0f;

			for (int i = 0; i < LayerCount; i++)
			{
				result += GetLayerInfluence(i);
			}

			_totalInfluence = result;

			return _totalInfluence;
		}

		private float GetNormalizedInfluence(int index)
		{
			return GetLayerInfluence(index) / TotalInfluence;
		}

		protected override float ApplyLayerNoise(float currentValue, FastNoisePipelineLayerContext context, float x, float y)
		{
			return currentValue + (context.layerNoise * GetNormalizedInfluence(context.layerIndex));
		}

		protected override float ApplyLayerNoise(float currentValue, FastNoisePipelineLayerContext context, float x, float y, float z)
		{
			return currentValue + (context.layerNoise * GetNormalizedInfluence(context.layerIndex));
		}

		protected override float NormalizeLayeredNoise2D(float currentValue)
		{
			return currentValue / LayerCount;
		}

		protected override float NormalizeLayeredNoise3D(float currentValue)
		{
			return currentValue / LayerCount;
		}

		public override void Validate()
		{
			base.Validate();
			if (!HasLayers) return;

			if (_layerInfluences == null || _layerInfluences.Length != LayerCount)
			{
				Array.Resize(ref _layerInfluences, LayerCount);
			}

			CalculateTotalInfluence();
		}
	}
}
