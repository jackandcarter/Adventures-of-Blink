using UnityEngine;

namespace AdventuresOfBlink.UI
{
    /// <summary>
    /// UI panel that lists items from the player's inventory.
    /// Items are populated under a content root using a prefab slot.
    /// </summary>
    public class InventoryPanel : MonoBehaviour
    {
        [Tooltip("Inventory system providing the data.")]
        public InventorySystem inventory;

        [Tooltip("Parent transform for generated item UI objects.")]
        public Transform contentRoot;

        [Tooltip("Prefab used for each item entry in the UI.")]
        public GameObject itemSlotPrefab;

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
        /// Rebuilds the item list based on the inventory contents.
        /// </summary>
        public void Refresh()
        {
            if (inventory == null || contentRoot == null || itemSlotPrefab == null)
                return;

            foreach (Transform child in contentRoot)
                Destroy(child.gameObject);

            foreach (var item in inventory.items)
            {
                GameObject go = Instantiate(itemSlotPrefab, contentRoot);
                ItemSlotUI slot = go.GetComponent<ItemSlotUI>();
                if (slot != null)
                    slot.Setup(item.data, item.quantity);
            }
        }
    }
}
