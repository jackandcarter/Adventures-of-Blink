using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace AdventuresOfBlink.Targeting
{
    /// <summary>
    /// Handles selecting and cycling between <see cref="Targetable"/> objects.
    /// </summary>
    #if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
    #endif
    public class TargetingSystem : MonoBehaviour
    {
        /// <summary>
        /// Currently selected target.
        /// </summary>
        public Targetable CurrentTarget { get; private set; }

#if ENABLE_INPUT_SYSTEM
        private PlayerInput playerInput;
        private InputAction cycleAction;
#endif

        /// <summary>
        /// Raised whenever <see cref="CurrentTarget"/> changes.
        /// </summary>
        public event System.Action<Targetable> TargetChanged;

#if ENABLE_INPUT_SYSTEM
        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            if (playerInput != null)
                cycleAction = playerInput.actions["CycleTarget"];
        }

        private void OnEnable()
        {
            if (cycleAction != null)
            {
                cycleAction.performed += OnCyclePerformed;
                cycleAction.Enable();
            }
        }

        private void OnDisable()
        {
            if (cycleAction != null)
            {
                cycleAction.performed -= OnCyclePerformed;
                cycleAction.Disable();
            }
        }
#endif

        private void Update()
        {
#if ENABLE_INPUT_SYSTEM
            if (cycleAction == null)
            {
                bool pressed = Keyboard.current != null && Keyboard.current[Key.Tab].wasPressedThisFrame;
                if (pressed)
                    CycleTarget();
            }
#else
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                CycleTarget();
            }
#endif
        }

#if ENABLE_INPUT_SYSTEM
        private void OnCyclePerformed(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
                CycleTarget();
        }
#endif

        /// <summary>
        /// Cycles to the next closest <see cref="Targetable"/> based on distance.
        /// </summary>
        public void CycleTarget()
        {
            Targetable[] targets = FindObjectsByType<Targetable>(FindObjectsSortMode.None);
            if (targets.Length == 0)
            {
                SetTarget(null);
                return;
            }

            System.Array.Sort(targets, (a, b) =>
            {
                float da = (a.transform.position - transform.position).sqrMagnitude;
                float db = (b.transform.position - transform.position).sqrMagnitude;
                return da.CompareTo(db);
            });

            int index = System.Array.IndexOf(targets, CurrentTarget);
            index = (index + 1) % targets.Length;
            SetTarget(targets[index]);
        }

        private void SetTarget(Targetable target)
        {
            if (CurrentTarget == target)
                return;

            CurrentTarget = target;
            TargetChanged?.Invoke(CurrentTarget);
        }
    }
}
