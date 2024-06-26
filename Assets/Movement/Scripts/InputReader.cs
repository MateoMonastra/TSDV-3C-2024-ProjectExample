using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement
{
    public class InputReader : MonoBehaviour
    {
        public RunningBehaviour runBehaviour;
        public JumpBehaviour jumpBehaviour;

        private void Awake()
        {
            if (runBehaviour == null)
            {
                Debug.LogError($"{name}: {nameof(runBehaviour)} is null!" +
                               $"\nThis class is dependant on a {nameof(runBehaviour)} component!");
            }
            if (jumpBehaviour == null)
            {
                Debug.LogError($"{name}: {nameof(jumpBehaviour)} is null!" +
                               $"\nThis class is dependant on a {nameof(jumpBehaviour)} component!");
            }
        }

        public void HandleMoveInput(InputAction.CallbackContext context)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
            if (runBehaviour != null)
                runBehaviour.Move(moveDirection);
        }
        public void HandleJumpInput(InputAction.CallbackContext context)
        {
            //context.phase == InputActionPhase.Started it's the same as context.started
            if (jumpBehaviour && context.started)
                jumpBehaviour.StartCoroutine(jumpBehaviour.JumpCoroutine());
        }
    }
}
