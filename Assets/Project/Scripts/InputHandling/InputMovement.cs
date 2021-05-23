using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Project.Scripts.InputHandling
{
    [Serializable]
    public class InputEventVector2 : UnityEvent<float, float> { }
    public class InputMovement : MonoBehaviour
    {
        public Controls.MovementActions movementActions;
        public static InputMovement Instance;

        public InputEventVector2 MoveInputEvent;
        public InputEventVector2 RotateInputEvent;

        private void Awake()
        {
            movementActions = Input.Controls.Movement;
            Instance = this;

            SetControls();
        }
        private void OnEnable()
        {
            EnableControls();
        }
        private void OnDisable()
        {
            DisableControls();
        }

        public void EnableControls()
        {
            movementActions.Enable();
        }
        public void DisableControls()
        {
            movementActions.Disable();
        }
        private void SetControls()
        {
            movementActions.Movement.performed += OnMove;
            movementActions.Movement.canceled += OnMove;
            movementActions.Rotate.performed += OnRotate;
            movementActions.Rotate.canceled += OnRotate;
        }

        private void OnMove(InputAction.CallbackContext moveAction)
        {
            var moveInput = moveAction.ReadValue<Vector2>();
            MoveInputEvent?.Invoke(moveInput.x, moveInput.y);
        }
        private void OnRotate(InputAction.CallbackContext context)
        {
            var rotateInput = Mouse.current.position.ReadValue();
            RotateInputEvent?.Invoke(rotateInput.x, rotateInput.y);
        }
    }
}
