using UnityEngine;
using AdventuresOfBlink.Data;

namespace AdventuresOfBlink.UI
{
    /// <summary>
    /// UI panel that displays abilities equipped to the dock shortcuts.
    /// </summary>
    public class DockPanel : MonoBehaviour
    {
        [Tooltip("Inventory system providing dock slot data.")]
        public InventorySystem inventory;

        [Tooltip("UI elements for each dock slot.")]
        public DockSlotUI[] slots;

        private void OnEnable()
        {
            if (inventory != null)
                inventory.InventoryChanged += Refresh;
            Refresh();
        }

        private void OnDisable()
        {
            if (inventory != null)
                inventory.InventoryChanged -= Refresh;
        }

        /// <summary>
        /// Updates each slot to match the inventory's dock assignments.
        /// </summary>
        public void Refresh()
        {
            if (inventory == null || slots == null)
                return;

            for (int i = 0; i < slots.Length; i++)
            {
                AbilityData ability = null;
                ItemData item = null;
                if (i < inventory.dockSlots.Count)
                {
                    ability = inventory.dockSlots[i].ability;
                    item = inventory.dockSlots[i].item;
                }

                if (slots[i] != null)
                {
                    slots[i].slotIndex = i;
                    slots[i].SetEntry(ability, item);
                }
            }
        }
    }
}
