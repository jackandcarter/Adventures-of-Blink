# UI Panels Guide

This guide explains how to assemble and connect the user interface components included with **Adventures of Blink**.

## Inventory System
1. Create an empty GameObject named `Managers` (or reuse an existing persistent object).
2. Add the `InventorySystem` component.
3. Other UI panels will reference this object to display and modify inventory data.

## Dock Panel
1. Drag **Dock.prefab** from `Assets/Prefabs` into your canvas.
2. Select the root **Dock** object and assign the scene's `InventorySystem` to **DockPanel.inventory**.
3. Expand the **slots** array and drag each child `DockSlotUI` into the list in left-to-right order.
4. During play, calls to `InventorySystem.EquipAbility` or `EquipItem` update each slot. The dock supports drag-and-drop between slots.

## Inventory Panel
1. Create a new UI panel under the canvas (`UI > Panel`).
2. Add a **Vertical Layout Group** component so entries arrange automatically.
3. Attach the `InventoryPanel` script.
4. Set **inventory** to your `InventorySystem` instance.
5. Create a prefab for item slots:
   - Add an `Image` for the icon and a **TMP_Text** for the quantity label.
   - Add `ItemSlotUI` and assign its `icon` and `quantityText` fields.
   - Save the object as `ItemSlot.prefab`.
6. Assign this prefab to **itemSlotPrefab** and set **contentRoot** to the panel's layout group transform.
7. The panel rebuilds whenever the inventory changes.

## Upgrade Panel
1. Add another panel object and attach `UpgradePanel`.
2. Set **upgradeSystem** to a scene object with an `UpgradeSystem` component.
3. Populate **availableUpgrades** with `UpgradeData` assets that define costs and rewards.
4. Create a prefab for upgrade entries containing an icon `Image`, two `Text` labels (name and cost), and a `Button`.
   - Attach `UpgradeEntryUI` and assign its fields.
5. Assign the prefab to **entryPrefab** and set **contentRoot** to the panel's layout group.
6. At runtime the panel populates entries, and pressing a button calls `UpgradeSystem.ApplyUpgrade`.

## Health Bars
1. To show floating health for characters, create a prefab with a `Slider` UI element.
2. Add the `HealthBar` component.
3. Assign the host's `CharacterStats` to **stats** and set **blinkTransform** to the player if visibility range is needed.
4. Enable **alwaysVisible** to keep the bar displayed regardless of distance.

Save this file inside the `Assets` folder so Unity imports it as a TextAsset.
