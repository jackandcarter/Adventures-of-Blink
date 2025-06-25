using UnityEngine;
using UnityEngine.AI;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace AdventuresOfBlink.Player
{
    /// <summary>
    /// Handles player movement using WSAD keys or point-and-click navigation.
    /// Parameters are exposed so they can be tuned in the Unity Inspector.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [Tooltip("Units per second moved when using keyboard input.")]
        public float moveSpeed = 5f;

        [Tooltip("Degrees per second for smooth rotation.")]
        public float rotationSpeed = 720f;

        private CharacterController controller;
        private NavMeshAgent agent;
        private Camera mainCamera;

        private Vector3 keyboardVelocity;
#if ENABLE_INPUT_SYSTEM
        private PlayerInput playerInput;
        private InputAction moveAction;
        private Vector2 moveInput;
#endif

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            agent = GetComponent<NavMeshAgent>();
            mainCamera = Camera.main;
#if ENABLE_INPUT_SYSTEM
            playerInput = GetComponent<PlayerInput>();
            if (playerInput != null)
            {
                moveAction = playerInput.actions["Move"];
                if (moveAction != null)
                {
                    moveAction.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
                    moveAction.canceled += ctx => moveInput = Vector2.zero;
                }
            }
#endif

            if (agent != null)
            {
                agent.updateRotation = false;
                agent.updateUpAxis = false;
            }
        }

        private void Update()
        {
            HandleKeyboardMovement();
            HandleClickMovement();
            RotateTowardsMovement();
        }

        private void HandleKeyboardMovement()
        {
#if ENABLE_INPUT_SYSTEM
            Vector2 input = moveInput;
#else
            Vector2 input = Vector2.zero;
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
#endif
            input = Vector2.ClampMagnitude(input, 1f);
            keyboardVelocity = new Vector3(input.x, 0f, input.y) * moveSpeed;

            if (keyboardVelocity.sqrMagnitude > 0.001f)
            {
                if (agent != null)
                {
                    agent.ResetPath();
                }
                controller.Move(keyboardVelocity * Time.deltaTime);
            }
        }

        private void HandleClickMovement()
        {
#if ENABLE_INPUT_SYSTEM
            bool clicked = Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame;
#else
            bool clicked = Input.GetMouseButtonDown(1);
#endif
            if (!clicked || agent == null || mainCamera == null)
                return;

#if ENABLE_INPUT_SYSTEM
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
#else
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
#endif
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        private void RotateTowardsMovement()
        {
            Vector3 direction = keyboardVelocity.sqrMagnitude > 0.001f
                ? keyboardVelocity
                : agent != null && agent.hasPath
                    ? agent.desiredVelocity
                    : Vector3.zero;

            if (direction.sqrMagnitude > 0.001f)
            {
                direction.y = 0f;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
