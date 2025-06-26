# Adventures of Blink

Adventures of Blink is a rogue-RPG built in **Unity 6.1**. By day you are Ben "Blink" Kade, a UI programmer at DataWorks. By night you become the CodeWarrior Blink, a vigilante who uses a custom shader implant to hide his identity and augment his senses. With help from Duke, a robotic min-pin companion, Blink hunts hostile hologram programs that threaten DataCity and the Zenith A.I. that keeps it stable.

Each sunset triggers Blink's transformation. Ben's implant rewrites his appearance with a shimmering energy skin, masking his identity and granting heightened reflexes. As daylight returns, the implant powers down and he reverts to his normal self. Duke accompanies him through these nightly hunts, providing sensor data and mechanical assistance.

Rogue holograms are corrupted routines that have escaped Zenith's control, manifesting as dangerous projections that can damage real-world systems. Blink and Duke track these anomalies across DataCity, purging them before they destabilize the network.

The project is in early setup. The vision includes hack-and-slash battles with a plasma sword, a companion ability system for Duke, a macOS-style dock for quick items and skills, weather effects, and upgrades for Blink's implant and Duke's parts. Movement will support WSAD or mouse input, with combo attacks triggered by timing.

## Repository Layout
- `Assets/` – Game assets and scripts
  - `Editor/` – Custom Unity editor tools
  - `Scripts/` – Gameplay code
  - `Scenes/` – Unity scenes
  - `Art/` – Artwork and textures
  - `Prefabs/` – Prefab assets
  - `Materials/` – Material definitions
  - `Systems/` – Core runtime systems
- `Packages/` – Unity package manifest
- `ProjectSettings/` – Unity project settings

## Getting Started
1. Clone or download this repository.
2. In Unity Hub, click **Add project** and select the cloned folder.
3. Open with **Unity 6.1**.

All gameplay assets and scripts will live under the `Assets/` folder. Subfolders such as `Scripts/` and `Art/` will grow as new systems are introduced.

Additional systems and tools will be added as development continues.
The new **InventorySystem** script tracks items and abilities and feeds data to UI panels, including the dock shortcut slots.
The **DayNightCycle** manager controls lighting over time, and **PlayerFormSwitcher** lets you use the Switch Form action (default `F`) at night to toggle between Ben and Blink.

## Controls
The project uses the Input System with the `InputSystem_Actions` asset.
Default bindings for the **Player** action map are:

- **Move** – WASD or gamepad left stick
- **Attack** – Left mouse button or gamepad west button
- **Interact** – `E` key or gamepad north button
- **Jump** – Space bar or gamepad south button
- **Crouch** – `C` key or gamepad east button
- **Sprint** – Left Shift or left stick press
- **Switch Form** – `F` key or gamepad left shoulder

## Saving and Loading
The `SaveGame` component persists the player's inventory and upgrade levels.
1. Place the script on a persistent object in your scene.
2. Assign your scene's `InventorySystem` and `DukeController` to the fields.
Data is stored as JSON at `Application.persistentDataPath/save.json` using
`SaveUtility`.

## Local Test Setup
Edit Mode unit tests can be executed using Docker. First run `scripts/setup.sh`
to pull the Unity editor image. Then set your `UNITY_LICENSE` environment
variable and execute `scripts/run-tests.sh`.

```bash
$ ./scripts/setup.sh
$ export UNITY_LICENSE="<your license string>"
$ ./scripts/run-tests.sh
```
