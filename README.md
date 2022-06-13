# Obsidize Fast Noise

This is a port of the
[Auburn/FastNoise](https://github.com/Auburn/FastNoiseLite/tree/master/CSharp)
C# implementation that adds tools for generating procedural noise configuration
assets in Unity.

This module was inspired by
[this GDC talk](https://www.youtube.com/watch?v=LWFzPP8ZbdU&ab_channel=GDC)
about noise generation, with the idea that noise values should be obtained in a
dimensional way.

```csharp
// Bad
// 1. no way to "look backwards"
// 2. values depend on previous values in the chain
// 3. cannot be multi-threaded (easily)
Random.Next();

// Better
// 1. always produces the same result given the same coordinates
// 2. output values are a combination of a starting seed and the given coordinates, so they are not chained together
// 3. much easier to multi-thread
NoiseMap.GetNoise(x, y);
```

## Installation

The easiest way to install this package is by downloading the main bundle under
`Bundles/`

## Usage

After installation, you will see a new option in the right-click / Create menu
named "Fast Noise". Use this menu to create your modules and pipelines as needed
(See sample assets under `Tests/` for ideas).

Once you create your modules / pipelines, get a reference to them in your script
and use the `CreateContext()` method to start generating noise.

```csharp
using UnityEngine;
using Obsidize.FastNoise;

public class ExampleScript : MonoBehaviour
{
	
	[SerializeField] private FastNoiseModule _noiseModule;
	private FastNoiseContext _noise;
	
	private void Start()
	{
		_noise = _noiseModule.CreateContext();
		_noise.SetSeed(12345);
		Debug.Log("some random point: " + noise.GetNoise(55f, 123.4f));
	}
}
```

## Core Concepts

This module contains two core asset concepts:

- `Module` - Atomic FastNoiseLite configurations that generate a single pass of
  noise
- `Pipeline` - Groups of Modules layered together with various "influence"
  weights. The more influence a module has, the more its noise shape is present
  in the output.

Note that a pipeline is itself a module, so you can place a pipeline as a layer
in another pipeline.

## Asset Previews

This module also provides a very useful "asset preview" texture to help
visualize the noise output that each configuration or pipeline will make.

Each module and pipeline asset will have a set of "preview" options to change
the visualization state.

## Customization

To adjust how noise values are obtained from a module, make a custom class that
extends `FastNoisPreset` and override the `GetNoise(x, y)` / `GetNoise(x, y, z)`
methods.

To make a custom value transformation module, extend the `FastNoisePipe` class
and override the `TransformNoise(v)` method.

(See `FastNoiseSuppressor` / `FastNoiseAmplifier` for example code)

To make a custom pipeline aggregator, extend the `FastNoisePipeline` class
