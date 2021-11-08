using System;
using UnityEngine;

namespace Obsidize.FastNoise
{

	[CreateAssetMenu(menuName = "Fast Noise/Pipelines/Influence", fileName = "FastNoiseInfluencePipeline")]
	public class FastNoiseInfluencePipeline : FastNoisePipeline
	{

		public class AggregatorSource : IFastNoiseAggregatorContextSource
		{

			private readonly IFastNoiseContext _context;
			private readonly float _influenceWeight;

			public AggregatorSource(FastNoiseModule module, float influenceWeight)
			{
				_context = module.CreateContext();
				_influenceWeight = influenceWeight;
			}

			public float CombineNoise(float accumulator, float x, float y)
			{
				return accumulator + (_context.GetNoise(x, y) * _influenceWeight);
			}

			public float CombineNoise(float accumulator, float x, float y, float z)
			{
				return accumulator + (_context.GetNoise(x, y, z) * _influenceWeight);
			}

			public void SetSeed(int seed)
			{
				_context.SetSeed(seed);
			}
		}

		[HideInInspector] [SerializeField] private float[] _layerInfluences;
		[HideInInspector] [SerializeField] private float _totalInfluence;

		public float TotalInfluence => _totalInfluence;

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

		protected override IFastNoiseAggregatorContextSource CreateAggregatorSource(FastNoiseModule module, int index)
		{
			return new AggregatorSource(module, GetNormalizedInfluence(index));
		}

		public override float NormalizeAggregatedNoise2D(float currentValue)
		{
			return currentValue / LayerCount;
		}

		public override float NormalizeAggregatedNoise3D(float currentValue)
		{
			return currentValue / LayerCount;
		}

		protected override void OnValidate()
		{
			base.OnValidate();
			if (!HasLayers) return;

			if (_layerInfluences == null || _layerInfluences.Length != LayerCount)
			{
				Array.Resize(ref _layerInfluences, LayerCount);
			}

			CalculateTotalInfluence();
		}
	}
}
