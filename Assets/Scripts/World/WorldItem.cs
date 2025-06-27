using UnityEngine;
using UnityEngine.EventSystems;
using AdventuresOfBlink.Data;

namespace AdventuresOfBlink.World
{
    /// <summary>
    /// Represents an item that exists in the world and can be picked up
    /// by the player.
    /// </summary>
    public class WorldItem : MonoBehaviour, IPointerClickHandler
    {
        [Tooltip("Item represented by this world object.")]
        public ItemData itemData;

        [Tooltip("Quantity granted when picked up.")]
        public int quantity = 1;

        /// <summary>
        /// Adds the item to the provided inventory and destroys this object.
        /// </summary>
        public void PickUp(InventorySystem inventory)
        {
            if (inventory == null || itemData == null)
                return;

            inventory.AddItem(itemData, quantity);
            Destroy(gameObject);
        }

        /// <summary>
        /// Displays a context menu when right-clicked in the world.
        /// </summary>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Right)
                return;

            InventorySystem inventory = FindObjectOfType<InventorySystem>();
            if (inventory == null)
                return;

            UI.ContextMenuUtility.Show("Pick Up", () => PickUp(inventory), eventData.position);
        }
    }
}
