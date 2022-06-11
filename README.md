# Obsidize Fast Noise

This is a port of the
[Auburn/FastNoise](https://github.com/Auburn/FastNoiseLite/tree/master/CSharp)
C# implementation that adds tools for generating procedural noise configuration
assets in Unity.

## Installation

This git repo is directly installable via the unity package manager. Simply
paste the repo url into the package manager "add" menu and unity will do the
rest.

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
