using System.Collections.Generic;
using UnityEngine;

namespace Obsidize.FastNoise
{
	public abstract class FastNoisePipeline : FastNoiseModule
	{

		public static bool IsPipeline(FastNoiseModule module)
		{
			return module != null && module is FastNoisePipeline;
		}

		public static bool IsPipelineDependency(FastNoiseModule container, FastNoiseModule target)
		{
			return IsPipeline(container) && (container as FastNoisePipeline).ContainsModule(target);
		}


		[SerializeField] private List<FastNoiseModule> _layers = new List<FastNoiseModule>();

		private readonly FastNoisePipelineLayerContext _cachedLayerContext = new FastNoisePipelineLayerContext();

		public int LayerCount => _layers.Count;
		public bool HasLayers => LayerCount > 0;

		protected abstract float ApplyLayerNoise(float currentValue, FastNoisePipelineLayerContext context, float x, float y);
		protected abstract float ApplyLayerNoise(float currentValue, FastNoisePipelineLayerContext context, float x, float y, float z);

		protected virtual float NormalizeLayeredNoise2D(float currentValue)
		{
			return currentValue;
		}

		protected virtual float NormalizeLayeredNoise3D(float currentValue)
		{
			return currentValue;
		}

		private void OnValidate()
		{
			Validate();
		}

		public FastNoiseModule GetLayerAt(int index)
		{
			return _layers[index];
		}

		public bool HasCircularReferenceTo(FastNoiseModule module)
		{
			return IsPipelineDependency(module, this);
		}

		private bool HasCircularReferenceToWithDebug(FastNoiseModule module)
		{

			var isCircularRef = HasCircularReferenceTo(module);

			if (isCircularRef)
			{
				Debug.LogWarning("Found circular reference from parent [" + this + "] to child [" + module + "]");
			}

			return isCircularRef;
		}

		public bool ContainsModule(FastNoiseModule module)
		{

			if (!HasLayers) return false;
			if (module == null) return false;

			foreach (var layer in _layers)
			{
				if (layer == null) continue;
				if (layer == module) return true;
				if (IsPipelineDependency(layer, module)) return true;
			}

			return false;
		}

		public override void DrawPreview(Texture2D texture)
		{
			if (HasLayers) base.DrawPreview(texture);
		}

		public override void Validate()
		{
			base.Validate();

			if (!HasLayers) return;

			_layers.RemoveAll(HasCircularReferenceToWithDebug);
		}

		public override void SetSeed(int seed)
		{
			if (!HasLayers) return;

			foreach (var layer in _layers)
			{
				layer?.SetSeed(seed);
			}
		}

		public override float GetNoise(float x, float y)
		{

			float result = 0f;

			for (int i = 0; i < LayerCount; i++)
			{
				_cachedLayerContext.layer = GetLayerAt(i);
				_cachedLayerContext.layerIndex = i;
				_cachedLayerContext.layerNoise = _cachedLayerContext.layer?.GetNoise(x, y) ?? 0f;
				result = ApplyLayerNoise(result, _cachedLayerContext, x, y);
			}

			return Mathf.Clamp01(NormalizeLayeredNoise2D(result));
		}

		public override float GetNoise(float x, float y, float z)
		{

			float result = 0f;

			for (int i = 0; i < LayerCount; i++)
			{
				_cachedLayerContext.layer = GetLayerAt(i);
				_cachedLayerContext.layerIndex = i;
				_cachedLayerContext.layerNoise = _cachedLayerContext.layer?.GetNoise(x, y, z) ?? 0f;
				result = ApplyLayerNoise(result, _cachedLayerContext, x, y, z);
			}

			return Mathf.Clamp01(NormalizeLayeredNoise3D(result));
		}
	}
}
