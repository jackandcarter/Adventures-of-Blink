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
            Refresh();
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
                if (i < inventory.dockSlots.Count)
                    ability = inventory.dockSlots[i].ability;

                if (slots[i] != null)
                    slots[i].SetAbility(ability);
            }
        }
    }
}
