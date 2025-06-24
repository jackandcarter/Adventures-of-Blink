using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
using AdventuresOfBlink.World;

namespace AdventuresOfBlink.Player
{
    /// <summary>
    /// Enables Blink to hack nearby <see cref="HackableDevice"/> objects
    /// using the interact key.
    /// </summary>
    public class PlayerHacking : MonoBehaviour
    {
        [Tooltip("How far Blink can reach to hack a device.")]
        public float interactionRange = 2f;

        [Tooltip("Layer mask used to identify hackable objects.")]
        public LayerMask hackMask = -1;

        [Tooltip("Keyboard key used to initiate hacking.")]
        public KeyCode hackKey = KeyCode.E;

        private HackableDevice activeDevice;
        private float hackTimer;
#if ENABLE_INPUT_SYSTEM
        private Key inputKey;
#endif
        private void Awake()
        {
#if ENABLE_INPUT_SYSTEM
            inputKey = (Key)hackKey;
#endif
        }

        private void Update()
        {
#if ENABLE_INPUT_SYSTEM
            bool pressed = Keyboard.current != null && Keyboard.current[inputKey].wasPressedThisFrame;
#else
            bool pressed = Input.GetKeyDown(hackKey);
#endif
            if (pressed)
            {
                TryStartHack();
            }

            if (activeDevice != null && hackTimer > 0f)
            {
                hackTimer -= Time.deltaTime;
                if (hackTimer <= 0f)
                    FinishTimedHack();
            }
        }

        private void TryStartHack()
        {
            HackableDevice device = FindDeviceInFront();
            if (device == null || device.IsHacked)
                return;

            device.BeginHack(this);
        }

        internal void StartTimedHack(HackableDevice device)
        {
            activeDevice = device;
            hackTimer = Mathf.Max(0f, device.hackTime);
            device.onHackStart?.Invoke();
        }

        private void FinishTimedHack()
        {
            activeDevice.CompleteHack();
            activeDevice = null;
            hackTimer = 0f;
        }

        private HackableDevice FindDeviceInFront()
        {
            Vector3 origin = transform.position + Vector3.up * 0.5f;
            if (Physics.Raycast(origin, transform.forward, out RaycastHit hit, interactionRange, hackMask))
            {
                return hit.collider.GetComponent<HackableDevice>();
            }
            return null;
        }
    }
}
