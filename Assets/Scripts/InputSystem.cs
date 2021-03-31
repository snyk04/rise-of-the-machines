using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public class InputEventVector2 : UnityEvent<float, float> { }
public class InputSystem : MonoBehaviour
{
    private Controls controls;

    public InputEventVector2 MoveInputEvent;
    public InputEventVector2 RotateInputEvent;

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Player.Enable();

        controls.Player.Movement.performed += OnMove;
        controls.Player.Movement.canceled += OnMove;
        controls.Player.Rotate.performed += OnRotate;
        controls.Player.Rotate.canceled += OnRotate;
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

    private void OnDisable()
    {
        controls.Player.Disable();
    }
}