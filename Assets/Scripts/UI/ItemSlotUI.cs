using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using AdventuresOfBlink.Data;
using AdventuresOfBlink.World;
using AdventuresOfBlink;

namespace AdventuresOfBlink.UI
{
    /// <summary>
    /// Displays a single item entry with icon and quantity text.
    /// </summary>
public class ItemSlotUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
    {
        public Image icon;
        public TMP_Text quantityText;

        [Tooltip("Inventory used for drop actions.")]
        public InventorySystem inventory;

        [Tooltip("Prefab spawned when dropping an item.")]
        public GameObject worldItemPrefab;

        private ItemData itemData;

        /// <summary>
        /// Initializes the slot visuals.
        /// </summary>
        public void Setup(ItemData data, int quantity)
        {
            itemData = data;

            if (icon != null)
                icon.sprite = data != null ? data.icon : null;

            if (quantityText != null)
                quantityText.text = quantity.ToString();
        }

        /// <summary>
        /// Starts dragging this item, storing it in the shared drag payload.
        /// </summary>
        public void OnBeginDrag(PointerEventData eventData)
        {
            DragPayload.draggedItem = itemData;
            DragPayload.sourceSlot = null;
        }

        /// <summary>
        /// Clears the drag payload when dragging ends.
        /// </summary>
        public void OnEndDrag(PointerEventData eventData)
        {
            DragPayload.Clear();
        }

        /// <summary>
        /// Shows a context menu when right-clicked allowing the item to be dropped.
        /// </summary>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Right || itemData == null)
                return;

            if (inventory == null || worldItemPrefab == null)
                return;

            ContextMenuUtility.Show("Drop Item", DropItem, eventData.position);
        }

        private void DropItem()
        {
            if (inventory.RemoveItem(itemData))
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Vector3 pos = player != null ? player.transform.position + player.transform.forward * 2f : Vector3.zero;
                GameObject go = Instantiate(worldItemPrefab, pos, Quaternion.identity);
                WorldItem wi = go.GetComponent<WorldItem>();
                if (wi != null)
                {
                    wi.itemData = itemData;
                    wi.quantity = 1;
                }
            }
        }
    }
}
