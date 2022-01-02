using System;
using UnityEngine;

namespace Obsidize.FastNoise
{

	[CreateAssetMenu(menuName = "Fast Noise/Pipelines/Influence", fileName = "FastNoiseInfluencePipeline")]
	public class FastNoiseInfluencePipeline : FastNoisePipeline
	{

		[HideInInspector] [SerializeField] private float[] _layerInfluences;
		[HideInInspector] [SerializeField] private float[] _layerInfluenceRatios;
		[HideInInspector] [SerializeField] private float _totalInfluence;

		public float TotalInfluence => _totalInfluence;

		protected override FastNoisePipelineLayer CreateLayer()
		{
			return new FastNoiseInfluencePipelineLayer(GetLayerInfluenceRatio);
		}

		public override float NormalizeCombinedNoise2D(float value)
		{
			return Mathf.Clamp01(value);
		}

		public override float NormalizeCombinedNoise3D(float value)
		{
			return Mathf.Clamp01(value);
		}

		protected override void OnValidate()
		{
			base.OnValidate();
			if (HasModules) CalculateTotalInfluence();
		}

		public float GetLayerInfluenceRatio(int layerIndex)
		{
			return _layerInfluenceRatios[layerIndex];
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

			var layerCount = ModuleCount;

			if (_layerInfluences == null || _layerInfluences.Length != layerCount)
			{
				Array.Resize(ref _layerInfluences, layerCount);
			}

			if (_layerInfluenceRatios == null || _layerInfluenceRatios.Length != layerCount)
			{
				Array.Resize(ref _layerInfluenceRatios, layerCount);
			}

			var result = 0f;

			for (int i = 0; i < layerCount; i++)
			{
				result += GetLayerInfluence(i);
			}

			_totalInfluence = result;

			// Precalculate the influence ratios so we can
			// avoid repetitive divisions during runtime.
			for (int i = 0; i < layerCount; i++)
			{
				_layerInfluenceRatios[i] = _layerInfluences[i] / _totalInfluence;
			}

			return _totalInfluence;
		}
	}
}
