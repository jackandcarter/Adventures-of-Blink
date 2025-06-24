# Setup Guide

This guide outlines how to configure core systems in **Adventures of Blink**. Follow these steps after creating a new scene.

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
3. `PlayerController` exposes two main fields:
   - `moveSpeed` – movement speed when using the keyboard.
   - `rotationSpeed` – how quickly the character turns toward movement.

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

## Inventory and UI
1. Add an `InventorySystem` component to a persistent object (e.g. an empty `Managers` GameObject).
2. Link UI panels to this inventory:
   - **DockPanel** – set its `inventory` reference so dock slots reflect equipped items or abilities.
   - **InventoryPanel** – also assign the same `InventorySystem` for listing items.
   - **UpgradePanel** – requires an `UpgradeSystem` (see below) but will also reference the inventory through that system.

## World Managers
1. Place a `DayNightCycle` component in the scene to animate lighting throughout the day.
2. Add a `WeatherController` if you want dynamic weather effects like rain.
3. Include an `UpgradeSystem` and connect its fields:
   - **inventory** – the `InventorySystem` created earlier.
   - **playerForms** – the `PlayerFormSwitcher` component on the player (if present).
   - **duke** – the `DukeController` instance.

Save this file inside the `Assets` folder so Unity imports it as a `TextAsset` and it can be viewed from the editor.
