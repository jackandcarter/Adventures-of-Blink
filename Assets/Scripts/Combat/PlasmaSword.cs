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
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class PlasmaSword : MonoBehaviour
    {
        [Tooltip("Ordered abilities making up the combo.")]
        public ComboSequence combo;

        [Tooltip("Stats of the attacker performing the combo.")]
        public RuntimeStats attacker;

        [Tooltip("Optional stats of the current target receiving damage.")]
        public RuntimeStats target;

        [Tooltip("Multiplier applied to attack speed from upgrades.")]
        public float speedMultiplier = 1f;

        private Animator animator;
        private int stepIndex;
        private float lastInputTime = -Mathf.Infinity;
#if ENABLE_INPUT_SYSTEM
        private PlayerInput playerInput;
        private InputAction attackAction;
#endif

        private void Awake()
        {
            animator = GetComponent<Animator>();
#if ENABLE_INPUT_SYSTEM
            playerInput = GetComponent<PlayerInput>();
            if (playerInput != null)
                attackAction = playerInput.actions["Attack"];
#endif
        }

        private void OnEnable()
        {
#if ENABLE_INPUT_SYSTEM
            if (attackAction != null)
                attackAction.performed += OnAttack;
#endif
        }

        private void OnDisable()
        {
#if ENABLE_INPUT_SYSTEM
            if (attackAction != null)
                attackAction.performed -= OnAttack;
#endif
        }

        private void Update()
        {
#if !ENABLE_INPUT_SYSTEM
            if (Input.GetKeyDown(KeyCode.Space))
                HandleAttack();
#endif
        }

#if ENABLE_INPUT_SYSTEM
        private void OnAttack(InputAction.CallbackContext ctx) => HandleAttack();
#endif

        private void HandleAttack()
        {
            if (combo == null || combo.steps.Count == 0)
                return;

            if (Time.time - lastInputTime > combo.inputWindow)
                stepIndex = 0;

            PerformAttack(combo.steps[stepIndex]);
            lastInputTime = Time.time;
            stepIndex = (stepIndex + 1) % combo.steps.Count;
        }

        private void PerformAttack(AbilityData ability)
        {
            if (attacker != null && ability.energyCost > 0)
            {
                if (!attacker.ConsumeEnergy(ability.energyCost))
                {
                    Debug.Log($"Not enough energy for {ability.abilityName}");
                    return;
                }
            }

            if (animator != null && ability.animationClip != null)
            {
                float speed = (attacker != null ? attacker.Speed : 1f) * speedMultiplier;
                animator.speed = speed;
                animator.Play(ability.animationClip.name);
            }

            if (attacker != null && target != null)
            {
                float damage = BattleFormula.CalculateDamage(attacker, target, ability);
                Health health = target.GetComponent<Health>();
                if (health != null)
                    health.ApplyDamage(Mathf.RoundToInt(damage));
                Debug.Log($"PlasmaSword dealt {damage} damage with {ability.abilityName}");
            }
        }
    }
}
