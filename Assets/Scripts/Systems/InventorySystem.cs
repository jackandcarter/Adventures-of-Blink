using System.Collections.Generic;
using UnityEngine;
using AdventuresOfBlink.Data;

namespace AdventuresOfBlink
{
    /// <summary>
    /// Stores items and abilities collected by the player and manages
    /// ability slots used by the dock shortcuts.
    /// </summary>
    public class InventorySystem : MonoBehaviour
    {
        [Header("Inventory")]
        [Tooltip("List of items in the player's inventory.")]
        public List<ItemEntry> items = new();

        [Tooltip("Abilities acquired by the player.")]
        public List<AbilityData> abilities = new();

        [Header("Dock Slots")]
        [Tooltip("Slots mapped to the dock for quick access.")]
        public List<DockSlot> dockSlots = new();

        /// <summary>
        /// Adds an item to the inventory or increases the quantity if it exists.
        /// </summary>
        public void AddItem(ItemData item, int quantity = 1)
        {
            if (item == null)
                return;

            ItemEntry entry = items.Find(e => e.data == item);
            if (entry != null)
            {
                entry.quantity += quantity;
            }
            else
            {
                items.Add(new ItemEntry { data = item, quantity = quantity });
            }
        }

        /// <summary>
        /// Removes an item and returns true if successful.
        /// </summary>
        public bool RemoveItem(ItemData item, int quantity = 1)
        {
            ItemEntry entry = items.Find(e => e.data == item);
            if (entry == null || entry.quantity < quantity)
                return false;

            entry.quantity -= quantity;
            if (entry.quantity <= 0)
                items.Remove(entry);
            return true;
        }

        /// <summary>
        /// Equips an ability to a dock slot.
        /// </summary>
        public void EquipAbility(int dockIndex, AbilityData ability)
        {
            if (dockIndex < 0 || dockIndex >= dockSlots.Count)
                return;

            dockSlots[dockIndex].ability = ability;
        }

        [System.Serializable]
        public class ItemEntry
        {
            public ItemData data;
            public int quantity = 1;
        }

        [System.Serializable]
        public class DockSlot
        {
            [Tooltip("Ability equipped to this slot.")]
            public AbilityData ability;
        }
    }
}
