using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using AdventuresOfBlink.Data;

namespace AdventuresOfBlink.UI
{
    /// <summary>
    /// Visual representation of a single dock slot.
    /// </summary>
    public class DockSlotUI : MonoBehaviour,
        IBeginDragHandler, IEndDragHandler,
        IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Tooltip("Image component displaying the ability or item icon.")]
        public Image icon;

        [Tooltip("Index of this slot within the dock.")]
        public int slotIndex;

        private AbilityData ability;
        private ItemData item;

        private Vector3 originalScale = Vector3.one;

        private void Awake()
        {
            originalScale = transform.localScale;
        }

        /// <summary>
        /// Updates the icon based on the assigned ability or item.
        /// </summary>
        public void SetEntry(AbilityData newAbility, ItemData newItem)
        {
            ability = newAbility;
            item = newItem;

            if (icon == null)
                return;

            Sprite sprite = null;
            if (ability != null)
                sprite = ability.icon;
            else if (item != null)
                sprite = item.icon;

            icon.sprite = sprite;
            icon.enabled = sprite != null;
        }

        /// <summary>
        /// Enlarges the slot icon on pointer hover for a dock magnification effect.
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = originalScale * 1.2f;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = originalScale;
        }

        /// <summary>
        /// Begins dragging the current entry if one exists.
        /// </summary>
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (ability == null && item == null)
                return;

            DragPayload.draggedAbility = ability;
            DragPayload.draggedItem = item;
            DragPayload.sourceSlot = this;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            DragPayload.Clear();
        }

        /// <summary>
        /// Handles a drop onto this slot from another slot or the inventory.
        /// Performs auto-rearranging when dragging between slots.
        /// </summary>
        public void OnDrop(PointerEventData eventData)
        {
            DockPanel panel = GetComponentInParent<DockPanel>();
            if (panel == null || panel.inventory == null || !DragPayload.HasPayload)
                return;

            var inventory = panel.inventory;

            if (DragPayload.sourceSlot != null)
            {
                int from = DragPayload.sourceSlot.slotIndex;
                int to = slotIndex;
                if (from != to)
                    inventory.MoveDockSlot(from, to);
            }
            else
            {
                inventory.EquipAbility(slotIndex, DragPayload.draggedAbility);
                inventory.EquipItem(slotIndex, DragPayload.draggedItem);
            }

            panel.Refresh();
            DragPayload.Clear();
        }
    }
}
