using System.Collections.Generic;
using System.IO;
using UnityEngine;
using AdventuresOfBlink.Data;
using AdventuresOfBlink.Companion;

namespace AdventuresOfBlink.Systems
{
    /// <summary>
    /// Handles saving and loading game data such as inventory contents
    /// and player upgrade levels using JsonUtility.
    /// </summary>
    public static class SaveUtility
    {
        private const string FileName = "save.json";

        /// <summary>
        /// Path to the save file in persistent data.
        /// </summary>
        public static string SavePath => Path.Combine(Application.persistentDataPath, FileName);

        /// <summary>
        /// Serializes the inventory and upgrades to disk.
        /// </summary>
        public static void Save(InventorySystem inventory, DukeController duke)
        {
            if (inventory == null)
                return;

            SaveData data = new()
            {
                items = new(),
                abilityNames = new(),
                dockSlots = new(),
                dukeLevels = new()
            };

            foreach (var item in inventory.items)
            {
                if (item.data == null) continue;
                data.items.Add(new ItemEntryData
                {
                    itemName = item.data.name,
                    quantity = item.quantity
                });
            }

            foreach (var ability in inventory.abilities)
            {
                if (ability != null)
                    data.abilityNames.Add(ability.name);
            }

            foreach (var slot in inventory.dockSlots)
            {
                DockSlotData slotData = new();
                if (slot.ability != null)
                    slotData.abilityName = slot.ability.name;
                if (slot.item != null)
                    slotData.itemName = slot.item.name;
                data.dockSlots.Add(slotData);
            }

            if (duke != null)
            {
                foreach (var abilitySlot in duke.abilities)
                    data.dukeLevels.Add(abilitySlot.level);
            }

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(SavePath, json);
        }

        /// <summary>
        /// Loads data from disk and applies it to the inventory and upgrades.
        /// </summary>
        public static void Load(InventorySystem inventory, DukeController duke)
        {
            if (inventory == null)
                return;
            if (!File.Exists(SavePath))
                return;

            string json = File.ReadAllText(SavePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            if (data == null)
                return;

            inventory.items.Clear();
            if (data.items != null)
            {
                foreach (var item in data.items)
                {
                    ItemData itemData = Resources.Load<ItemData>(item.itemName);
                    if (itemData != null)
                    {
                        inventory.items.Add(new InventorySystem.ItemEntry
                        {
                            data = itemData,
                            quantity = item.quantity
                        });
                    }
                }
            }

            inventory.abilities.Clear();
            if (data.abilityNames != null)
            {
                foreach (var abilityName in data.abilityNames)
                {
                    AbilityData ability = Resources.Load<AbilityData>(abilityName);
                    if (ability != null)
                        inventory.abilities.Add(ability);
                }
            }

            inventory.dockSlots.Clear();
            if (data.dockSlots != null)
            {
                foreach (var slotData in data.dockSlots)
                {
                    var slot = new InventorySystem.DockSlot();
                    if (!string.IsNullOrEmpty(slotData.abilityName))
                        slot.ability = Resources.Load<AbilityData>(slotData.abilityName);
                    if (!string.IsNullOrEmpty(slotData.itemName))
                        slot.item = Resources.Load<ItemData>(slotData.itemName);
                    inventory.dockSlots.Add(slot);
                }
            }

            if (duke != null && data.dukeLevels != null)
            {
                for (int i = 0; i < data.dukeLevels.Count && i < duke.abilities.Count; i++)
                    duke.abilities[i].level = data.dukeLevels[i];
            }

            inventory.InvokeInventoryChanged();
        }

        [System.Serializable]
        private class SaveData
        {
            public List<ItemEntryData> items;
            public List<string> abilityNames;
            public List<DockSlotData> dockSlots;
            public List<int> dukeLevels;
        }

        [System.Serializable]
        private class ItemEntryData
        {
            public string itemName;
            public int quantity;
        }

        [System.Serializable]
        private class DockSlotData
        {
            public string abilityName;
            public string itemName;
        }
    }
}
