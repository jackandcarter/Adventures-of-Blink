# Setup Guide

This guide outlines how to configure core systems in **Adventures of Blink**. Follow these steps after creating a new scene.

## Package Dependencies

The project relies on several Unity packages. Ensure these appear in `Packages/manifest.json` with the versions below:

| Package | Version | Purpose |
| --- | --- | --- |
| `com.unity.inputsystem` | 1.14.0 | Player input handling. |
| `com.unity.render-pipelines.universal` | 17.1.0 | Universal Render Pipeline (URP). |
| `com.unity.textmeshpro` | 3.0.1 | Rich text for UI and dialogues. |
| `com.unity.ai.navigation` | 2.0.8 | NavMesh pathfinding utilities. |
| `com.unity.timeline` | 1.8.7 | Sequencing cutscenes. |
| `com.unity.test-framework` | 1.5.1 | Running edit mode tests. |
| `com.unity.ugui` | 2.0.0 | Built‑in UI system. |
| `com.unity.visualscripting` | 1.9.7 | Visual scripting support. |
| `com.unity.ide.rider` | 3.0.36 | JetBrains Rider integration. |
| `com.unity.ide.visualstudio` | 2.0.23 | Visual Studio integration. |
| `com.unity.collab-proxy` | 2.8.2 | Unity Collaborate service. |
| `com.unity.multiplayer.center` | 1.0.0 | Multiplayer session tools. |

### Optional packages

- `com.unity.probuilder` – level prototyping.
- `com.unity.cinemachine` – advanced camera control.
- `com.unity.postprocessing` – post‑processing effects.
- `com.unity.terrain-tools` – terrain editing utilities.

### Additional systems

Add these components to your scene for full functionality:

- `DayNightCycle` – animates lighting over time.
- `WeatherController` – toggles rain and ambient changes.
- `SaveGame` – loads and saves player data.

## Enable the Input System
1. Install the **Input System** package if it is not already listed in `Packages/manifest.json`.
2. Open **Edit > Project Settings > Input System Package** and assign `Assets/InputSystem_Actions.inputactions` as the default actions asset.
3. In **Player** settings set **Active Input Handling** to `Input System Package` (or `Both`).

## Player
1. Create a `Player` GameObject.
2. Add the following components in this order:
   - `CharacterController`
   - `NavMeshAgent`
   - `PlayerController`
   - `PlayerInput` (assign `InputSystem_Actions` and set **Default Map** to `Player`)
3. `PlayerController` exposes two main fields:
   - `moveSpeed` – movement speed when using the keyboard.
   - `rotationSpeed` – how quickly the character turns toward movement.
4. Default controls using the **Player** action map:
   - **Move** – WASD or left stick
   - **Attack** – Left mouse button or gamepad west button
   - **Interact** – `E` key or gamepad north button
   - **Jump** – Space bar or gamepad south button
   - **Crouch** – `C` key or gamepad east button
   - **Sprint** – Left Shift or left stick press
   - **Switch Form** – `F` key or gamepad left shoulder

## Camera
1. Create a camera prefab or add a `CameraController` component to an existing camera.
2. Set the following fields on `CameraController`:
   - **target** – reference to the player transform.
   - **distance** – how far the camera sits behind the player.
   - **returnSpeed** – rate at which the camera eases back after orbiting.

## Duke Companion
1. Create a GameObject for Duke and attach the `DukeController` script.
2. Assign the player to the **target** field so Duke can follow.
3. Populate the **Abilities** list with `DukeAbilityData` assets to define Duke's actions.

## Targeting System
1. Add a `TargetingSystem` component to the player or a persistent manager object.
2. Attach `Targetable` to each enemy or NPC and set their **displayName**. Add a `Health` component if the object has hit points.
3. Create a panel named `TargetPanel` and attach the `TargetPanel` script.
   - Assign your `TargetingSystem` and hook up the name and health UI elements.
4. Press **Tab** in play mode to cycle between nearby targets. See [Targeting Guide](TargetingGuide.md) for full details.

## Inventory and UI
1. Add an `InventorySystem` component to a persistent object (e.g. an empty `Managers` GameObject).
2. Link UI panels to this inventory:
   - **DockPanel** – set its `inventory` reference so dock slots reflect equipped items or abilities. See [Dock Guide](DockGuide.md) for detailed setup.
   - **InventoryPanel** – also assign the same `InventorySystem` for listing items.
   - **UpgradePanel** – requires an `UpgradeSystem` (see below) but will also reference the inventory through that system.
   - For a walkthrough of each UI component see [UI Panels Guide](UIPanelsGuide.md).
3. Place your `ItemData` assets under `Assets/Resources/Items` and `AbilityData` assets under `Assets/Resources/Abilities`. This allows `SaveUtility` to locate them using `Resources.Load`.
4. Right-click any item slot in the inventory to open a context menu and select **Drop Item**. The item spawns in front of the player as a `WorldItem` that can be picked up later. Right-click a `WorldItem` in the scene and choose **Pick Up** to collect it.

## World Managers
1. Place a `DayNightCycle` component in the scene to animate lighting throughout the day.
2. Add a `WeatherController` if you want dynamic weather effects like rain.
3. Include an `UpgradeSystem` and connect its fields:
   - **inventory** – the `InventorySystem` created earlier.
   - **playerForms** – the `PlayerFormSwitcher` component on the player (if present).
   - **duke** – the `DukeController` instance.

Save this file inside the `Assets` folder so Unity imports it as a `TextAsset` and it can be viewed from the editor.

## Rendering Pipeline and Post-Processing
1. The project uses **Universal Render Pipeline (URP)**. Ensure the pipeline asset under `Assets/Settings/PC_RPAsset.asset` is assigned in **Graphics Settings**.
2. Import the **Post Processing** package if it isn't already included.
3. Create a global volume and assign `DefaultVolumeProfile.asset` for bloom, color grading and chromatic aberration. Enable bloom to accentuate neon lighting.

## Neon Materials
1. Two example shaders live in `Assets/Shaders`:
   - `NeonGlow.shader` – simple unlit shader with emission.
   - `HologramBarrier.shader` – transparent shader used for Blink's shield effect.
2. Create materials that use these shaders and adjust the emission colors to taste.
3. Assign the `NeonGlow` material to street lights or signs for a vibrant glow.

## Blink Hologram Effect
1. Add a `BlinkHologram` component to Blink's model.
2. Set **Hologram Material** to a material that uses `HologramBarrier.shader`.
3. Call `SetActive(true)` when Blink transforms to enable the barrier and `SetActive(false)` to return to the normal material.

## Environment Prototyping
1. Install the **ProBuilder** package from the package manager.
2. Use ProBuilder shapes to block out buildings and streets. Save meshes to the `Assets/Models` folder for reuse.
3. Example models `NeonSign.obj` and `WallPanel.obj` demonstrate simple planes for signage and walls.
4. See the [World Building Guide](WorldBuildingGuide.md) for detailed terrain and
   modular construction steps.

## Cinemachine Camera
1. Add a `CinemachineBrain` component to the main camera.
2. Create a `CinemachineVirtualCamera` and set **Follow** and **Look At** to the player.
3. Adjust the virtual camera's lens settings and damping values for smooth orbiting.
4. `CameraController` can coexist with Cinemachine so you still have manual control when needed.

## Trigger Volumes
1. Add a `TriggerVolume` component to a collider marked **Is Trigger**.
2. Use **onPlayerEnter** and **onPlayerExit** to hook up reactions in the Inspector.
3. `TeleportTrigger` moves the player to its **Target Location** when entered.
4. `BoundaryTrigger` can push the player back or stop their movement.
5. `SceneChangeTrigger` loads the specified scene using **Scene Name** on enter.
6. `CutsceneTrigger` plays a Timeline via its **Playable Director** on enter.

Save this file inside the `Assets` folder so Unity imports it as a `TextAsset` and it can be viewed from the editor.
