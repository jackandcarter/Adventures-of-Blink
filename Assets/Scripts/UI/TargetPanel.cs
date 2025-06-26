using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AdventuresOfBlink.Targeting;

namespace AdventuresOfBlink.UI
{
    /// <summary>
    /// UI panel that displays the currently targeted object.
    /// </summary>
    public class TargetPanel : MonoBehaviour
    {
        [Tooltip("Targeting system providing events.")]
        public TargetingSystem targetingSystem;

        [Tooltip("Text element showing the target name.")]
        public TMP_Text nameText;

        [Tooltip("Slider displaying target health.")]
        public Slider healthSlider;

        private void OnEnable()
        {
            if (targetingSystem != null)
                targetingSystem.TargetChanged += OnTargetChanged;
            OnTargetChanged(targetingSystem != null ? targetingSystem.CurrentTarget : null);
        }

        private void OnDisable()
        {
            if (targetingSystem != null)
                targetingSystem.TargetChanged -= OnTargetChanged;
        }

        private void OnTargetChanged(Targetable target)
        {
            if (nameText != null)
                nameText.text = target != null ? target.displayName : string.Empty;

            if (healthSlider != null)
            {
                if (target != null && target.stats != null)
                {
                    healthSlider.value = 1f;
                    healthSlider.gameObject.SetActive(true);
                }
                else
                {
                    healthSlider.value = 0f;
                    healthSlider.gameObject.SetActive(false);
                }
            }
        }
    }
}
