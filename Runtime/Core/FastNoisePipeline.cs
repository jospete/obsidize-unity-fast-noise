using System.Collections.Generic;
using UnityEngine;

namespace Obsidize.FastNoise
{
	public abstract class FastNoisePipeline : FastNoiseModule, IFastNoiseAggregatorContextOperator
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

		public int LayerCount => _layers.Count;
		public bool HasLayers => LayerCount > 0;

		protected abstract IFastNoiseAggregatorContextSource CreateAggregatorSource(FastNoiseModule module, int index);

		public virtual float NormalizeAggregatedNoise2D(float value)
		{
			return value;
		}

		public virtual float NormalizeAggregatedNoise3D(float value)
		{
			return value;
		}

		protected override void OnValidate()
		{
			base.OnValidate();
			if (HasLayers) _layers.RemoveAll(HasCircularReferenceToWithDebug);
		}

		public override FastNoiseContext CreateContext()
		{
			var sourceCount = LayerCount;
			var sources = new IFastNoiseAggregatorContextSource[sourceCount];

			for (int i = 0; i < sourceCount; i++)
			{
				sources[i] = CreateAggregatorSource(_layers[i], i);
			}

			return new FastNoiseAggregatorContext(sources, this);
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
	}
}
