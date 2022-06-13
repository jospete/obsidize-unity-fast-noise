# CHANGELOG

## 3.0.3

- Add static bundle export as alternative asset import option

## 3.0.2

- Documentation updates
- Fix dependency declaration

## 3.0.1

- Fix dependency com.obsidize.unity.range-input -> com.obsidize.range-input
  (Since apparently unity freaks out if "unity" is in the name of a non-unity
  package)
- Move samples out of the scope of this repo since they only serve to cause
  installation issues
- Normalize editor namespace from `Obsidize.FastNoise.EditorTools` ->
  `Obsidize.FastNoise.Editor`
- Fix misc issues with package declaration

## 3.0.0

- Add dependency com.obsidize.unity.range-input
- Remove internal MinMaxRange implementation in favor of new dependency
  implementation

## 2.3.2

- Fix editor tools assembly definition to only target editor platform
- Update Noise Terrain 3D Sample to be fully deployable

## 2.3.1

- Rename FastNoiseLiteUtility to the more appropriate FastNoiseLiteExtensions
- Replace hard-coded min (0f) and max (1f) values with shared constant
  references

## 2.3.0

- Add PlaneMeshBuilder, PlaneMeshBuilder2D and PlaneMeshBuilder3D utility
  classes for better noise visualization support.

## 2.2.0

- Add 3D Terrain Generator Sample Project

## 2.1.0

- Add preview option to show noise in 1D variation
- Documentation updates

## 2.0.2

- Fix circular dependency checking logic that did not account for some cases,
  which would end up crashing the unity editor

## 2.0.1

- Fix issue where FastNoisePipeline was producing an adaptive context for
  playmode (adaptive context is only meant for editor mode)

## 2.0.0

- Overhaul core FastNoiseModule implementations to require context creation, so
  that multiple pipeline / module instances can run independently from the same
  assets
- Documentation updates
- Code cleanup

## 1.2.0

- Refactor Pipeline implementation to allow for generalized sub-classes
- Add pipeline types: Additive, Influence (Previously default Pipeline
  implementation)
- Add module type: Inverter

## 1.1.1

- Adjust default asset values
- Update documentation

## 1.1.0

- Further unify base classes for ease of extension
- Add new modules: FastNoiseSuppressor, FastNoiseAmplifier

## 1.0.6

- Add utility methods to FastNoisePipelineModule for autofilling from Vector***
  constructs

## 1.0.5

- Documentation updates
- Code formatting

## 1.0.4

- Add option to disable domain warping
- Add tooltips for all FastNoiseOptions properties

## 1.0.3

- Update documentation

## 1.0.2

- Fix samples

## 1.0.0

- Initial release
