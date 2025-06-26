# UI Panels Guide

This guide explains how to assemble and connect the user interface components included with **Adventures of Blink**.

## Inventory System
1. Create an empty GameObject named `Managers` (or reuse an existing persistent object).
2. Add the `InventorySystem` component.
3. Other UI panels will reference this object to display and modify inventory data. `InventorySystem` exposes an `InventoryChanged` event used by panels to refresh when items change.

## Dock Panel
### Creating from Scratch
1. Under the canvas add a `Panel` named `Dock`.
   - Set **Anchor Min** and **Anchor Max** to `(0.5, 0)`.
   - Set **Pivot** to `(0.5, 0)` and size the rect wide enough for the slot icons.
2. Add child objects for each dock slot.
   - Give each slot an `Image` component.
   - Attach `DockSlotUI` and set **icon** to the slot's image. Set **slotIndex** in order starting at `0`.
3. On the root panel add the `DockPanel` component.
   - Assign the scene's `InventorySystem` to **inventory**.
   - Size the **slots** array and drag each `DockSlotUI` from the child objects into the list.
   - Optionally add a layout group to arrange the slots horizontally.

Dock slots update automatically through the `InventorySystem.InventoryChanged` event and support drag-and-drop via `DockSlotUI`.

## Inventory Panel
1. Add a `Panel` named `InventoryPanel` under the canvas.
   - Anchor it to the left side (`Anchor Min` `(0,1)` and `Anchor Max` `(0,1)` with pivot `(0,1)`).
   - Set a width of about `240` and height `300` (adjust as needed).
2. Add `Image` for the background if desired.
3. Add a **Vertical Layout Group** and **Content Size Fitter** (set **Vertical Fit** to `Preferred Size`) so entries stack top-to-bottom.
4. Attach the `InventoryPanel` script.
   - Assign **inventory** to your `InventorySystem`.
   - Set **contentRoot** to the panel's transform (or a child transform used for layout).
   - Assign **itemSlotPrefab** to the prefab described below.
5. Create an item slot prefab:
   - Add an `Image` named `Icon` and a **TMP_Text** named `Quantity`.
   - Attach `ItemSlotUI` and wire **icon** and **quantityText**.
   - Save as `ItemSlot.prefab` and reference it from `InventoryPanel`.
6. When `InventorySystem.InventoryChanged` fires, the panel rebuilds its item list.

## Upgrade Panel
1. Create a `Panel` named `UpgradePanel` and anchor it to the right side of the screen.
   - Example anchors: `Anchor Min` `(1,1)`, `Anchor Max` `(1,1)`, pivot `(1,1)` with a size similar to the inventory panel.
2. Add a **Vertical Layout Group** to stack upgrade entries.
3. Attach the `UpgradePanel` script.
   - Assign a scene object containing `UpgradeSystem` to **upgradeSystem**.
   - Set **contentRoot** to the layout transform.
   - Assign **entryPrefab** as described below.
   - Fill **availableUpgrades** with `UpgradeData` assets.
4. Create an upgrade entry prefab:
   - Include an `Image` for the icon, two `Text` components for the name and cost, and a `Button`.
   - Attach `UpgradeEntryUI` and link its fields.
5. `UpgradePanel` populates entries on enable and each button calls `UpgradeSystem.ApplyUpgrade`. The upgrade system references the same `InventorySystem` to deduct materials and unlock abilities.

## Health Bars
1. Make a prefab with a `Slider` UI element sized to your liking.
2. Attach `HealthBar` and assign:
   - **slider** – the slider component.
   - **stats** – `RuntimeStats` of the actor.
   - **blinkTransform** – optional; Blink's transform for visibility range.
3. Set **alwaysVisible** if you want the bar shown regardless of distance.

Save this file inside the `Assets` folder so Unity imports it as a TextAsset.
