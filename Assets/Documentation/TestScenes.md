# Test Scenes

This guide explains how to assemble a quick test scene in **Adventures of Blink**. Follow these steps to spawn the player and Duke, set up the dock UI, and place interactive objects for rapid gameplay testing.

## Player and Companion
1. Create an empty `Player` GameObject.
2. Add the following components:
   - `CharacterController`
   - `NavMeshAgent`
   - `PlayerController`
3. Tune these fields on **PlayerController**:
   - `moveSpeed` – keyboard movement speed.
   - `rotationSpeed` – how quickly the player turns.
4. Create a `Duke` GameObject and attach `DukeController`.
5. Assign the player transform to **target** and adjust:
   - `followSpeed` – movement speed toward the player.
   - `followDistance` – minimum distance maintained.
   - `abilities` – list of `DukeAbilitySlot` entries.
6. Optionally add `PlayerFormSwitcher` to the player and link the **cycle** field to a `DayNightCycle` instance.

## Dock UI
1. Drag **Dock.prefab** from `Assets/Prefabs` into your canvas.
2. On the root **Dock** object, locate `DockPanel` and set:
   - **inventory** – reference to your scene's `InventorySystem`.
   - **slots** – array of child `DockSlotUI` components.
3. Each `DockSlotUI` exposes:
   - `icon` – `Image` used to display ability or item icons.
   - `slotIndex` – order of the slot in the dock.
4. Press play and the dock will mirror the inventory's equipped items and abilities.

## Hackable Devices
1. Add a `HackableDevice` component to any object the player can interact with.
2. Key inspector fields:
   - `hackTime` – seconds required for a timed hack.
   - `useMinigame` – toggle to launch a hacking minigame.
   - `onHackStart` and `onHackComplete` – events invoked during the hack.
3. Use `PlayerHacking` on the player to trigger hacks when in range.

## Trigger Volumes
1. For simple events, attach `TriggerVolume` to a collider set as **Is Trigger** and hook up:
   - `onPlayerEnter`
   - `onPlayerExit`
2. Variant components provide additional behaviour:
   - `TeleportTrigger` – moves the player to **targetLocation**.
   - `BoundaryTrigger` – set **pushPlayer**, **pushDistance**, and **blockMovement**.
   - `SceneChangeTrigger` – loads the scene specified in **sceneName**.
   - `CutsceneTrigger` – plays a Timeline via **director**.

## Weather and Day/Night
1. Add `DayNightCycle` and assign a directional light to **sunLight**. Configure:
   - `lightColor` and `lightIntensity` curves
   - `dayLength` – seconds for a full cycle
2. Drop in a `WeatherController` and set:
   - `sunLight` – same light used by the day/night cycle.
   - `rainPrefab` – particle system for rain effects.
   - `clearAmbientColor` and `rainAmbientColor` – ambient lighting presets.
   - `rainLightMultiplier` – intensity scale while raining.
3. Call `SetWeather()` to switch between `Clear` and `Rain` during play.

With these elements in place you can rapidly test movement, hacking, and UI features inside new scenes.
