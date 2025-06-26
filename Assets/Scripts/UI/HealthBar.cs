using UnityEngine;
using UnityEngine.UI;
using AdventuresOfBlink.Data;

namespace AdventuresOfBlink.UI
{
    /// <summary>
    /// Displays a floating health bar for a character.
    /// The bar becomes visible when Blink is within range.
    /// </summary>
    public class HealthBar : MonoBehaviour
    {
        [Tooltip("Slider UI element used to show health percentage.")]
        public Slider slider;

        [Tooltip("Character stats providing max health.")]
        public RuntimeStats stats;

        [Tooltip("Current health value.")]
        public int currentHealth;

        [Tooltip("Transform of Blink used to determine visibility range.")]
        public Transform blinkTransform;

        [Tooltip("Distance from Blink required to show the bar.")]
        public float visibleRange = 5f;

        [Tooltip("Keep the bar visible regardless of distance.")]
        public bool alwaysVisible;

        private void Awake()
        {
            if (stats != null)
                currentHealth = stats.MaxHealth;
            UpdateBar();
        }

        private void Update()
        {
            if (!alwaysVisible && blinkTransform != null && slider != null)
            {
                float sqrRange = visibleRange * visibleRange;
                bool show = (blinkTransform.position - transform.position).sqrMagnitude <= sqrRange;
                if (slider.gameObject.activeSelf != show)
                    slider.gameObject.SetActive(show);
            }

            UpdateBar();
        }

        /// <summary>
        /// Applies damage and refreshes the UI.
        /// </summary>
        public void ApplyDamage(int amount)
        {
            currentHealth = Mathf.Max(0, currentHealth - amount);
            UpdateBar();
        }

        /// <summary>
        /// Restores health up to the maximum value.
        /// </summary>
        public void Heal(int amount)
        {
            int max = stats != null ? stats.MaxHealth : currentHealth;
            currentHealth = Mathf.Min(max, currentHealth + amount);
            UpdateBar();
        }

        private void UpdateBar()
        {
            if (slider == null)
                return;
            int max = stats != null ? stats.MaxHealth : currentHealth;
            slider.value = max > 0 ? (float)currentHealth / max : 0f;
        }
    }
}
