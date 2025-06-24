using AdventuresOfBlink.Data;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace AdventuresOfBlink.Combat
{
    /// <summary>
    /// Handles timed combo attacks with Blink's plasma sword.
    /// Uses ComboSequence assets to define attack steps.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class PlasmaSword : MonoBehaviour
    {
        [Tooltip("Ordered abilities making up the combo.")]
        public ComboSequence combo;

        [Tooltip("Stats of the attacker performing the combo.")]
        public CharacterStats attacker;

        [Tooltip("Optional stats of the current target receiving damage.")]
        public CharacterStats target;

        [Tooltip("Multiplier applied to attack speed from upgrades.")]
        public float speedMultiplier = 1f;

        private Animator animator;
        private int stepIndex;
        private float lastInputTime = -Mathf.Infinity;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
#if ENABLE_INPUT_SYSTEM
            bool pressed = Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;
#else
            bool pressed = Input.GetKeyDown(KeyCode.Space);
#endif
            if (!pressed || combo == null || combo.steps.Count == 0)
                return;

            if (Time.time - lastInputTime > combo.inputWindow)
                stepIndex = 0;

            PerformAttack(combo.steps[stepIndex]);
            lastInputTime = Time.time;
            stepIndex = (stepIndex + 1) % combo.steps.Count;
        }

        private void PerformAttack(AbilityData ability)
        {
            if (animator != null && ability.animationClip != null)
            {
                float speed = (attacker != null ? attacker.speed : 1f) * speedMultiplier;
                animator.speed = speed;
                animator.Play(ability.animationClip.name);
            }

            if (attacker != null && target != null)
            {
                float damage = BattleFormula.CalculateDamage(attacker, target, ability);
                Debug.Log($"PlasmaSword dealt {damage} damage with {ability.abilityName}");
            }
        }
    }
}
