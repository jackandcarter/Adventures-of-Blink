using System.Collections;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace AdventuresOfBlink.CameraSystem
{
    /// <summary>
    /// Allows the player to orbit the camera around a target by dragging the mouse.
    /// When the mouse button is released, the camera smoothly returns to its default angle.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [Header("Orbit Settings")]
        [Tooltip("Target transform to orbit around.")]
        public Transform target;

        [Tooltip("Distance from the target.")]
        public float distance = 5f;

        [Tooltip("Degrees per pixel while dragging.")]
        public float orbitSpeed = 0.2f;

        [Tooltip("Mouse button used for dragging. 0=Left, 1=Right, 2=Middle")]
        public int mouseButton = 1;

        [Tooltip("Speed that the camera returns to the default orientation.")]
        public float returnSpeed = 2f;

        private Vector2 defaultAngles;
        private Vector2 currentAngles;
        private Coroutine returnRoutine;

        private void Awake()
        {
            Vector3 euler = transform.eulerAngles;
            defaultAngles = new Vector2(euler.x, euler.y);
            currentAngles = defaultAngles;
            UpdatePosition();
        }

        private void Update()
        {
            bool dragging = IsDragging();
            if (dragging)
            {
                Vector2 delta = GetMouseDelta();
                currentAngles.y += delta.x * orbitSpeed;
                currentAngles.x -= delta.y * orbitSpeed;
                currentAngles.x = Mathf.Clamp(currentAngles.x, -80f, 80f);
                UpdatePosition();

                if (returnRoutine != null)
                {
                    StopCoroutine(returnRoutine);
                    returnRoutine = null;
                }
            }
            else if (WasReleased())
            {
                StartReturnRoutine();
            }
        }

        private bool IsDragging()
        {
#if ENABLE_INPUT_SYSTEM
            if (Mouse.current == null)
                return false;
            return GetButton(mouseButton).isPressed;
#else
            return Input.GetMouseButton(mouseButton);
#endif
        }

        private bool WasReleased()
        {
#if ENABLE_INPUT_SYSTEM
            if (Mouse.current == null)
                return false;
            return GetButton(mouseButton).wasReleasedThisFrame;
#else
            return Input.GetMouseButtonUp(mouseButton);
#endif
        }

        private Vector2 GetMouseDelta()
        {
#if ENABLE_INPUT_SYSTEM
            return Mouse.current != null ? Mouse.current.delta.ReadValue() : Vector2.zero;
#else
            return new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
#endif
        }

#if ENABLE_INPUT_SYSTEM
        private ButtonControl GetButton(int index)
        {
            return index switch
            {
                0 => Mouse.current.leftButton,
                1 => Mouse.current.rightButton,
                _ => Mouse.current.middleButton
            };
        }
#endif

        private void StartReturnRoutine()
        {
            if (returnRoutine != null)
                StopCoroutine(returnRoutine);
            returnRoutine = StartCoroutine(ReturnToDefault());
        }

        private IEnumerator ReturnToDefault()
        {
            while (Vector2.Distance(currentAngles, defaultAngles) > 0.01f)
            {
                currentAngles = Vector2.Lerp(currentAngles, defaultAngles, Time.deltaTime * returnSpeed);
                UpdatePosition();
                yield return null;
            }
            currentAngles = defaultAngles;
            UpdatePosition();
            returnRoutine = null;
        }

        private void UpdatePosition()
        {
            if (target == null)
                return;
            Quaternion rot = Quaternion.Euler(currentAngles.x, currentAngles.y, 0f);
            Vector3 offset = rot * new Vector3(0f, 0f, -distance);
            transform.position = target.position + offset;
            transform.rotation = rot;
        }
    }
}
