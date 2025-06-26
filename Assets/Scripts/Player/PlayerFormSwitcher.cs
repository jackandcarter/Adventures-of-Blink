using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
using AdventuresOfBlink.Data;
using AdventuresOfBlink.Systems;

namespace AdventuresOfBlink.Player
{
    /// <summary>
    /// Allows toggling between Ben and Blink forms at night.
    /// Models and stats are swapped when the hotkey is pressed.
    /// </summary>
    public class PlayerFormSwitcher : MonoBehaviour
    {
        [Tooltip("Day/night cycle reference for time checks.")]
        public DayNightCycle cycle;

        [Header("Form Objects")]
        [Tooltip("GameObject representing Ben.")]
        public GameObject benModel;
        [Tooltip("GameObject representing Blink.")]
        public GameObject blinkModel;

        [Header("Form Stats")]
        [Tooltip("Base stats asset for Ben.")]
        public CharacterStats benStats;
        [Tooltip("Base stats asset for Blink.")]
        public CharacterStats blinkStats;

        [Tooltip("Runtime stats for the active form.")]
        public RuntimeStats currentStats;

        private RuntimeStats benRuntime;
        private RuntimeStats blinkRuntime;

        public RuntimeStats BenRuntime => benRuntime;
        public RuntimeStats BlinkRuntime => blinkRuntime;

        [Tooltip("Keyboard key used to switch forms.")]
        public KeyCode switchKey = KeyCode.F;

        private bool isBlink;
#if ENABLE_INPUT_SYSTEM
        private Key inputKey;
        private PlayerInput playerInput;
        private InputAction switchAction;
#endif

        private void Awake()
        {
#if ENABLE_INPUT_SYSTEM
            inputKey = (Key)switchKey;
            playerInput = GetComponent<PlayerInput>();
            if (playerInput != null)
                switchAction = playerInput.actions["SwitchForm"];
#endif
            benRuntime = new RuntimeStats(benStats);
            blinkRuntime = new RuntimeStats(blinkStats);
            SetForm(false);
        }

#if ENABLE_INPUT_SYSTEM
        private void OnEnable()
        {
            if (switchAction != null)
            {
                switchAction.performed += OnSwitchPerformed;
                switchAction.Enable();
            }
        }

        private void OnDisable()
        {
            if (switchAction != null)
            {
                switchAction.performed -= OnSwitchPerformed;
                switchAction.Disable();
            }
        }
#endif

        private void Update()
        {
#if ENABLE_INPUT_SYSTEM
            bool pressed = false;
            if (switchAction == null)
                pressed = Keyboard.current != null && Keyboard.current[inputKey].wasPressedThisFrame;
#else
            bool pressed = Input.GetKeyDown(switchKey);
#endif
            if (pressed && cycle != null && cycle.IsNight)
            {
                SetForm(!isBlink);
            }
        }

#if ENABLE_INPUT_SYSTEM
        private void OnSwitchPerformed(InputAction.CallbackContext ctx)
        {
            if (ctx.performed && cycle != null && cycle.IsNight)
                SetForm(!isBlink);
        }
#endif

        private void SetForm(bool blink)
        {
            isBlink = blink;
            if (benModel != null)
                benModel.SetActive(!blink);
            if (blinkModel != null)
                blinkModel.SetActive(blink);
            currentStats = blink ? blinkRuntime : benRuntime;
        }
    }
}
