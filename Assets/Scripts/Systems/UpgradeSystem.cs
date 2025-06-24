using UnityEngine;
using AdventuresOfBlink.Data;
using AdventuresOfBlink.Player;
using AdventuresOfBlink.Companion;

namespace AdventuresOfBlink.Systems
{
    /// <summary>
    /// Consumes inventory materials to unlock new abilities or improve stats.
    /// </summary>
    public class UpgradeSystem : MonoBehaviour
    {
        [Tooltip("Inventory containing upgrade materials and abilities.")]
        public InventorySystem inventory;

        [Tooltip("Player form switcher used to access Blink stats.")]
        public PlayerFormSwitcher playerForms;

        [Tooltip("Duke controller for hardware upgrades.")]
        public DukeController duke;

        /// <summary>
        /// Returns true if the inventory contains all required materials.
        /// </summary>
        public bool CanApply(UpgradeData upgrade)
        {
            if (upgrade == null || inventory == null)
                return false;

            if (upgrade.requirements != null)
            {
                foreach (var req in upgrade.requirements)
                {
                    if (req.item == null)
                        continue;
                    var entry = inventory.items.Find(e => e.data == req.item);
                    if (entry == null || entry.quantity < req.amount)
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Attempts to consume materials and apply the upgrade effects.
        /// </summary>
        public bool ApplyUpgrade(UpgradeData upgrade)
        {
            if (!CanApply(upgrade))
                return false;

            if (upgrade.requirements != null)
            {
                foreach (var req in upgrade.requirements)
                    inventory.RemoveItem(req.item, req.amount);
            }

            if (upgrade.abilityUnlock != null && !inventory.abilities.Contains(upgrade.abilityUnlock))
                inventory.abilities.Add(upgrade.abilityUnlock);

            if (upgrade.statBoost != null && playerForms != null && playerForms.blinkStats != null)
            {
                var stats = playerForms.blinkStats;
                stats.maxHealth += upgrade.statBoost.health;
                stats.attack += upgrade.statBoost.attack;
                stats.defense += upgrade.statBoost.defense;
                stats.speed += upgrade.statBoost.speed;
            }

            if (upgrade.dukeAbilityIndex >= 0 && duke != null)
                duke.UpgradeAbility(upgrade.dukeAbilityIndex);

            return true;
        }
    }
}
