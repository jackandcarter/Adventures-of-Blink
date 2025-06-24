using UnityEngine;
using UnityEngine.UI;
using AdventuresOfBlink.Data;

namespace AdventuresOfBlink.UI
{
    /// <summary>
    /// Displays a single item entry with icon and quantity text.
    /// </summary>
    public class ItemSlotUI : MonoBehaviour
    {
        public Image icon;
        public Text quantityText;

        /// <summary>
        /// Initializes the slot visuals.
        /// </summary>
        public void Setup(ItemData data, int quantity)
        {
            if (icon != null)
                icon.sprite = data != null ? data.icon : null;

            if (quantityText != null)
                quantityText.text = quantity.ToString();
        }
    }
}
