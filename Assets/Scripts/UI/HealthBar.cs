using UnityEngine;
using UnityEngine.UI;
using AdventuresOfBlink.Combat;

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

        [Tooltip("Health component providing values.")]
        public Health health;

        [Tooltip("Transform of Blink used to determine visibility range.")]
        public Transform blinkTransform;

        [Tooltip("Distance from Blink required to show the bar.")]
        public float visibleRange = 5f;

        [Tooltip("Keep the bar visible regardless of distance.")]
        public bool alwaysVisible;

        private void Awake()
        {
            if (health != null)
                health.HealthChanged += OnHealthChanged;
            UpdateBar();
        }

        private void OnDestroy()
        {
            if (health != null)
                health.HealthChanged -= OnHealthChanged;
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

        private void OnHealthChanged(int current, int max) => UpdateBar();

        private void UpdateBar()
        {
            if (slider == null || health == null)
                return;
            slider.value = health.Ratio;
        }
    }
}
