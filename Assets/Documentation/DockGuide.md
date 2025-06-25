# Dock Guide

This guide explains how to add and configure the dock UI used for quick access to abilities and items.

## Adding the Dock Prefab
1. Locate **Dock.prefab** under `Assets/Prefabs` and drag it into your canvas or UI root.
2. Select the root **Dock** object and in the **DockPanel** component assign your scene's `InventorySystem` to the **inventory** field.
3. Expand the **slots** list on `DockPanel` and link each child `DockSlotUI` in order from left to right.
4. Each child named *Slot* already has a `DockSlotUI` component with a preset `slotIndex`. Verify the indices start at `0` so the dock updates correctly.

## Drag and Drop Behaviour
1. `DockSlotUI` implements Unity drag interfaces and works together with the static `DragPayload` class.
2. When dragging begins, the slot stores its ability or item in `DragPayload` along with a reference to the source slot.
3. Dropping onto another slot calls `InventorySystem.MoveDockSlot` if the source was another dock slot, otherwise it equips the dragged entry using `EquipAbility` or `EquipItem`.
4. The dock refreshes after each drop and `DragPayload.Clear()` is called to reset the payload when the drag ends.

With the prefab placed and fields linked, the dock will display the current assignments from the inventory and support drag-and-drop rearranging.
