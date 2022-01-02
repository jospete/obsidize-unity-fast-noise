using UnityEngine;

namespace Obsidize.FastNoise
{
	public static class FastNoiseModuleExtensions
	{

		public static bool IsPipe(this FastNoiseModule module)
		{
			return module != null && module is FastNoisePipe;
		}

		public static bool IsPipeline(this FastNoiseModule module)
		{
			return module != null && module is FastNoisePipeline;
		}

		public static bool IsSelfRef(this FastNoiseModule module, FastNoiseModule other)
		{
			return module != null && other != null && module == other;
		}

		public static bool HasPipeDependency(this FastNoiseModule container, FastNoiseModule target)
		{
			return IsPipe(container) && ContainsModule(container as FastNoisePipe, target);
		}

		public static bool HasPipelineDependency(this FastNoiseModule container, FastNoiseModule target)
		{
			return IsPipeline(container) && ContainsModule(container as FastNoisePipeline, target);
		}

		public static bool HasRecursiveDependency(this FastNoiseModule container, FastNoiseModule target)
		{
			return HasPipeDependency(container, target) || HasPipelineDependency(container, target);
		}

		public static bool HasCircularReferenceTo(this FastNoiseModule parent, FastNoiseModule module)
		{
			return IsSelfRef(module, parent) || HasRecursiveDependency(module, parent);
		}

		public static bool HasCircularReferenceToWithDebug(this FastNoiseModule parent, FastNoiseModule module)
		{

			var isCircularRef = HasCircularReferenceTo(parent, module);

			if (isCircularRef)
			{
				Debug.LogWarning("Found circular reference from parent [" + parent + "] to child [" + module + "]");
			}

			return isCircularRef;
		}

		public static bool ContainsModule(this FastNoisePipe pipe, FastNoiseModule module)
		{

			if (pipe == null) return false;
			if (module == null) return false;
			if (pipe.InputSource == module) return true;

			return HasRecursiveDependency(pipe.InputSource, module);
		}

		public static bool ContainsModule(this FastNoisePipeline pipeline, FastNoiseModule module)
		{

			if (pipeline == null) return false;
			if (module == null) return false;
			if (!pipeline.HasModules) return false;

			foreach (var pipelineModule in pipeline.Modules)
			{
				if (pipelineModule == null) continue;
				if (pipelineModule == module) return true;
				if (HasRecursiveDependency(pipelineModule, module)) return true;
			}

			return false;
		}
	}
}
