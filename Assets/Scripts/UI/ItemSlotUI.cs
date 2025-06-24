using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using AdventuresOfBlink.Data;

namespace AdventuresOfBlink.UI
{
    /// <summary>
    /// Displays a single item entry with icon and quantity text.
    /// </summary>
    public class ItemSlotUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        public Image icon;
        public Text quantityText;

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
    }
}
