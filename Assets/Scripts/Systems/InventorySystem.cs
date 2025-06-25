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
        /// Raised whenever the inventory contents or dock slots change.
        /// </summary>
        public event System.Action InventoryChanged;

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

            InventoryChanged?.Invoke();
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

            InventoryChanged?.Invoke();
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
            dockSlots[dockIndex].item = null;

            InventoryChanged?.Invoke();
        }

        /// <summary>
        /// Equips an item to a dock slot.
        /// </summary>
        public void EquipItem(int dockIndex, ItemData data)
        {
            if (dockIndex < 0 || dockIndex >= dockSlots.Count)
                return;

            dockSlots[dockIndex].item = data;
            if (data != null)
                dockSlots[dockIndex].ability = null;

            InventoryChanged?.Invoke();
        }

        /// <summary>
        /// Moves a dock slot entry from one index to another, preserving order.
        /// </summary>
        public void MoveDockSlot(int from, int to)
        {
            if (from < 0 || from >= dockSlots.Count || to < 0 || to >= dockSlots.Count)
                return;

            var entry = dockSlots[from];
            dockSlots.RemoveAt(from);
            dockSlots.Insert(to, entry);

            InventoryChanged?.Invoke();
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

            [Tooltip("Item equipped to this slot.")]
            public ItemData item;
        }
    }
}
