# World Building Guide

This guide covers methods for laying out levels in **Adventures of Blink**.

## Unity Terrain
1. Create a `Terrain` object via **GameObject > 3D Object > Terrain**.
2. Select the terrain and in the **Inspector** use the **Raise/Lower Terrain** brush to sculpt hills.
3. Switch to the **Paint Texture** tab and add Terrain Layers to paint ground textures.
4. Use the **Paint Trees** tab to place tree prefabs across the terrain.
5. Open the **Paint Details** tab to scatter grass or detail meshes.

## Modular Building with ProBuilder
1. Install **ProBuilder** from the Package Manager if it is not already present.
2. Open the **ProBuilder** window and create shapes like cubes or stairs to form walls and floors.
3. When a section is finished choose **ProBuilder > Export > To Mesh** and drag the result into `Assets/Prefabs/Environment` as a prefab.
4. Reuse these prefabs to assemble larger structures.

## Quick Blockouts
- Built‑in primitives (Cube, Sphere, Plane, etc.) provide a fast way to test layouts.
- The **Grid** and **Tilemap** tools are useful for top‑down or tiled environments.
