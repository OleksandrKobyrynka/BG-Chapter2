using UnityEngine;
using UnityEngine.InputSystem;

public class SphereMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private InputActionReference _moveAction;
    private Vector2 _moveInput;

    private void OnEnable()
    {
        SubscribeEvents();
        _moveAction.action.Enable();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
        _moveInput = Vector2.zero;
    }

    private void SubscribeEvents()
    {
        _moveAction.action.performed += OnMovePerformed;
        _moveAction.action.canceled += OnMoveCanceled;
    }

    private void UnsubscribeEvents()
    {
        _moveAction.action.performed -= OnMovePerformed;
        _moveAction.action.canceled -= OnMoveCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _moveInput = Vector2.zero;
    }

    private void Update()
    {
        Vector3 move = new Vector3(_moveInput.x, 0, _moveInput.y);
        transform.Translate(move * _speed * Time.deltaTime);
    }
}