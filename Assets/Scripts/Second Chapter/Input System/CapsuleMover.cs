using UnityEngine;
using UnityEngine.InputSystem;

public class CapsuleMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private Vector2 _moveInput;

    private void OnMove(InputValue value)
    {
        if (!this.enabled)
        {
            return;
        }

        _moveInput = value.Get<Vector2>();
    }

    private void OnDisable()
    {
        _moveInput = Vector2.zero;
    }

    private void Update()
    {
        Vector3 direction = new Vector3(_moveInput.x, 0, _moveInput.y);
        transform.Translate(direction * _speed * Time.deltaTime);
    }
}