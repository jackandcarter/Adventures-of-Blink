using UnityEngine;
using UnityEngine.UI;
using AdventuresOfBlink.Data;

namespace AdventuresOfBlink.UI
{
    /// <summary>
    /// Visual representation of a single dock slot.
    /// </summary>
    public class DockSlotUI : MonoBehaviour
    {
        [Tooltip("Image component displaying the ability icon.")]
        public Image icon;

        /// <summary>
        /// Updates the icon based on the assigned ability.
        /// </summary>
        public void SetAbility(AbilityData ability)
        {
            if (icon == null)
                return;

            icon.sprite = ability != null ? ability.icon : null;
            icon.enabled = ability != null;
        }
    }
}
