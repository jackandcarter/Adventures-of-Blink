using UnityEngine;
using AdventuresOfBlink.Systems;
using AdventuresOfBlink.Data;

namespace AdventuresOfBlink.UI
{
    /// <summary>
    /// Displays available upgrades and allows the player to purchase them.
    /// </summary>
    public class UpgradePanel : MonoBehaviour
    {
        [Tooltip("Upgrade system used to apply upgrades.")]
        public UpgradeSystem upgradeSystem;

        [Tooltip("List of upgrades shown in this panel.")]
        public UpgradeData[] availableUpgrades;

        [Tooltip("Parent transform for generated entry UI objects.")]
        public Transform contentRoot;

        [Tooltip("Prefab used for each upgrade entry.")]
        public GameObject entryPrefab;

        private void OnEnable()
        {
            Refresh();
        }

        /// <summary>
        /// Rebuilds the UI entries based on the available upgrades.
        /// </summary>
        public void Refresh()
        {
            if (contentRoot == null || entryPrefab == null || availableUpgrades == null)
                return;

            foreach (Transform child in contentRoot)
                Destroy(child.gameObject);

            foreach (var upg in availableUpgrades)
            {
                GameObject go = Instantiate(entryPrefab, contentRoot);
                UpgradeEntryUI ui = go.GetComponent<UpgradeEntryUI>();
                if (ui != null)
                    ui.Setup(upg, upgradeSystem);
            }
        }
    }
}
