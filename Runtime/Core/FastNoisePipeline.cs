using System.Collections.Generic;
using UnityEngine;

namespace Obsidize.FastNoise
{
	public abstract class FastNoisePipeline : FastNoiseModule, IFastNoiseAggregatorContextOperator<FastNoisePipelineLayer>
	{

		[SerializeField] private List<FastNoiseModule> _modules;
		private FastNoisePipelineLayer[] _layers;

		public int ModuleCount => _modules != null ? _modules.Count : 0;
		public bool HasModules => ModuleCount > 0;
		public int LayerCount => _layers != null ? _layers.Length : 0;
		public bool HasLayers => LayerCount > 0;
		public bool LayersAreDesynced => ModuleCount != LayerCount;
		public IReadOnlyCollection<FastNoisePipelineLayer> Sources => _layers;

		protected abstract FastNoisePipelineLayer CreateLayer();

		public virtual float NormalizeCombinedNoise2D(float value)
		{
			return value;
		}

		public virtual float NormalizeCombinedNoise3D(float value)
		{
			return value;
		}

		public override FastNoiseContext CreateContext()
		{

			NormalizeLayers();

			// When not in play mode, use an adaptive context to be
			// more lenient on editor changes.
			return Application.isPlaying
				? new FastNoiseAggregatorContext<FastNoisePipelineLayer>(this)
				: new AdaptiveFastNoiseAggregatorContext<FastNoisePipelineLayer>(this);
		}

		private FastNoisePipelineLayer EnsureLayerAt(int index)
		{
			var layer = _layers[index];

			if (layer != null)
			{
				return layer;
			}

			return _layers[index] = CreateLayer();
		}

		protected override void OnValidate()
		{
			base.OnValidate();

			if (!HasModules) return;

			if (_modules == null) _modules = new List<FastNoiseModule>();
			_modules.RemoveAll(HasCircularReferenceToWithDebug);

			NormalizeLayers();
		}

		public void CheckForLayerDesync()
		{
			if (LayersAreDesynced) NormalizeLayers();
		}

		public void NormalizeLayers()
		{

			var moduleCount = _modules != null ? _modules.Count : 0;

			if (_layers == null)
			{
				_layers = new FastNoisePipelineLayer[moduleCount];
			}

			if (_layers.Length != moduleCount)
			{
				System.Array.Resize(ref _layers, moduleCount);
			}

			for (int i = 0; i < moduleCount; i++)
			{
				EnsureLayerAt(i).Initialize(_modules[i], i);
			}
		}

		public FastNoiseModule GetModuleAt(int index)
		{
			return _modules[index];
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

			if (!HasModules) return false;
			if (module == null) return false;

			foreach (var mod in _modules)
			{
				if (mod == null) continue;
				if (mod == module) return true;
				if (IsPipelineDependency(mod, module)) return true;
			}

			return false;
		}

		public static bool IsPipeline(FastNoiseModule module)
		{
			return module != null && module is FastNoisePipeline;
		}

		public static bool IsPipelineDependency(FastNoiseModule container, FastNoiseModule target)
		{
			return IsPipeline(container) && (container as FastNoisePipeline).ContainsModule(target);
		}
	}
}
