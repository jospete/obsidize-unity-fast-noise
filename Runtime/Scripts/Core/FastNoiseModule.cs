using UnityEngine;

namespace Obsidize.FastNoise
{
	/// <summary>
	/// Base class for all Obsidize.FastNoise constructs that 
	/// can produce a FastNoiseContext.
	/// 
	/// Any number of contexts can be produced from a single module,
	/// and each instance is guaranteed to be unique.
	/// 
	/// This allows for re-using the same FastNoise asset with 
	/// multiple different seeds simultaneously during gameplay.
	/// </summary>
	public abstract class FastNoiseModule : ScriptableObject
	{

		[SerializeField]
		private FastNoisePreviewOptions _preview;

		/// <summary>
		/// Baked-in asset preview options for this module.
		/// NOTE: This is intended for editor asset preview usage only.
		/// </summary>
		public FastNoisePreviewOptions Preview => _preview;


		/// <summary>
		/// Core entry point for noise generation.
		/// Creates a generalized context instance that will produce
		/// noise values similar to the ones shown in this asset's preview texture.
		/// </summary>
		public abstract FastNoiseContext CreateContext();

		/// <summary>
		/// Allows sub-classes to absorb the required parent class validators.
		/// </summary>
		protected virtual void OnValidate()
		{
			Preview?.Validate();
		}
	}
}
