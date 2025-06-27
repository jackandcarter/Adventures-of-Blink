# Dock Guide

This guide explains how to add and configure the dock UI used for quick access to abilities and items.

## Creating the Dock Panel
Follow these steps to construct the dock UI from scratch:
1. Create a `Canvas` and under it add a `Panel` named `Dock`.
   - Anchor the panel to the bottom center of the screen (set **Anchor Min** and **Anchor Max** to `(0.5, 0)` and pivot to `(0.5, 0)`).
   - Resize the panel so it has enough width for all slots in a row.
2. Add child objects for each slot you need, for example `Slot1`, `Slot2`, etc.
   - Position them evenly across the panel or use a layout group.
3. On each slot, add an `Image` component to display the icon and attach the `DockSlotUI` script.
   - Drag the slot's `Image` into the **icon** field of `DockSlotUI` and set **slotIndex** starting at `0`.
4. Select the root **Dock** object and add the `DockPanel` component.
   - Assign your scene's `InventorySystem` to **inventory**.
   - Size the **slots** array and drag each `DockSlotUI` from the child slots into the list.


## Drag and Drop Behaviour
1. `DockSlotUI` implements Unity drag interfaces and works together with the static `DragPayload` class.
2. When dragging begins, the slot stores its ability or item in `DragPayload` along with a reference to the source slot.
3. Dropping onto another slot calls `InventorySystem.MoveDockSlot` if the source was another dock slot, otherwise it equips the dragged entry using `EquipAbility` or `EquipItem`.
4. The dock refreshes after each drop and `DragPayload.Clear()` is called to reset the payload when the drag ends.

Once the panel and slots are linked, the dock will display the current assignments from the inventory and support drag-and-drop rearranging.
