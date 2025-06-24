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
        public CharacterStats benStats;
        public CharacterStats blinkStats;

        [Tooltip("Active stats used by other systems.")]
        public CharacterStats currentStats;

        [Tooltip("Keyboard key used to switch forms.")]
        public KeyCode switchKey = KeyCode.Tab;

        private bool isBlink;
#if ENABLE_INPUT_SYSTEM
        private Key inputKey;
#endif

        private void Awake()
        {
#if ENABLE_INPUT_SYSTEM
            inputKey = (Key)switchKey;
#endif
            SetForm(false);
        }

        private void Update()
        {
#if ENABLE_INPUT_SYSTEM
            bool pressed = Keyboard.current != null && Keyboard.current[inputKey].wasPressedThisFrame;
#else
            bool pressed = Input.GetKeyDown(switchKey);
#endif
            if (pressed && cycle != null && cycle.IsNight)
            {
                SetForm(!isBlink);
            }
        }

        private void SetForm(bool blink)
        {
            isBlink = blink;
            if (benModel != null)
                benModel.SetActive(!blink);
            if (blinkModel != null)
                blinkModel.SetActive(blink);
            currentStats = blink ? blinkStats : benStats;
        }
    }
}
