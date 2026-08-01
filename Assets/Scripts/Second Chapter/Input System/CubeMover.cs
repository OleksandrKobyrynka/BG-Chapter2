using UnityEngine;

public class CubeMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private InputSystem_Actions _inputSystem;

    private void Awake()
    {
        _inputSystem = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _inputSystem.Enable();
    }

    private void OnDisable()
    {
        _inputSystem.Disable();
    }

    private void Update()
    {
        Vector2 moveInput = _inputSystem.Player.Move.ReadValue<Vector2>();
        Move(moveInput);
    }

    private void Move(Vector2 direction)
    {
        Vector3 move = new Vector3(direction.x, 0, direction.y);
        transform.Translate(move * _speed * Time.deltaTime);
    }
}