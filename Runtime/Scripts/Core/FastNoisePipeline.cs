using System.Collections.Generic;
using UnityEngine;

namespace Obsidize.FastNoise
{
	public abstract class FastNoisePipeline : FastNoiseModule, IFastNoiseAggregatorContextOperator<FastNoisePipelineLayer>
	{

		[Space]
		[Header("Pipeline Options")]
		[SerializeField]
		private List<FastNoiseModule> _modules;

		private FastNoisePipelineLayer[] _layers;

		public IReadOnlyCollection<FastNoiseModule> Modules => _modules;
		public int ModuleCount => _modules != null ? _modules.Count : 0;
		public bool HasModules => ModuleCount > 0;

		public IReadOnlyCollection<FastNoisePipelineLayer> Sources => _layers;
		public int LayerCount => _layers != null ? _layers.Length : 0;
		public bool HasLayers => LayerCount > 0;

		public bool LayersAreDesynced => ModuleCount != LayerCount;

		protected abstract FastNoisePipelineLayer CreateLayer();

		public virtual float NormalizeCombinedNoise2D(float value)
		{
			return value;
		}

		public virtual float NormalizeCombinedNoise3D(float value)
		{
			return value;
		}

		public FastNoiseModule GetModuleAt(int index)
		{
			return _modules[index];
		}

		public void CheckForLayerDesync()
		{
			if (LayersAreDesynced) NormalizeLayers();
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
			_modules.RemoveAll(this.HasCircularReferenceToWithDebug);

			NormalizeLayers();
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
	}
}
