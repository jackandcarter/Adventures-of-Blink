using UnityEngine;

namespace AdventuresOfBlink.Combat
{
    /// <summary>
    /// Simple component storing current and maximum health.
    /// Other systems apply damage or healing through this API.
    /// </summary>
    public class Health : MonoBehaviour
    {
        [Tooltip("Maximum health value at full health.")]
        public int maxHealth = 100;

        [Tooltip("Current health value.")]
        public int currentHealth;

        /// <summary>
        /// Invoked when health changes with (current, max).
        /// </summary>
        public event System.Action<int, int> HealthChanged;

        private void Awake()
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            HealthChanged?.Invoke(currentHealth, maxHealth);
        }

        /// <summary>
        /// Decreases health by the given amount.
        /// </summary>
        public void ApplyDamage(int amount)
        {
            currentHealth = Mathf.Max(0, currentHealth - amount);
            HealthChanged?.Invoke(currentHealth, maxHealth);
        }

        /// <summary>
        /// Restores health up to the maximum value.
        /// </summary>
        public void Heal(int amount)
        {
            currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
            HealthChanged?.Invoke(currentHealth, maxHealth);
        }

        /// <summary>
        /// Returns the current health ratio 0-1.
        /// </summary>
        public float Ratio => maxHealth > 0 ? (float)currentHealth / maxHealth : 0f;
    }
}
