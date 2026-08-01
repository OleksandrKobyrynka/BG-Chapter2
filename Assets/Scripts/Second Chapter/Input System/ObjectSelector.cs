using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectSelector : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _cubeMover;
    [SerializeField] private MonoBehaviour _sphereMover;
    [SerializeField] private MonoBehaviour _capsuleMover;

    private InputSystem_Actions _inputSystem;

    private void Awake()
    {
        _inputSystem = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        SubscribeEvents();
        _inputSystem.Enable();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
        _inputSystem.Disable();
    }

    private void SubscribeEvents()
    {
        _inputSystem.Player.SelectCube.performed += OnSelectCube;
        _inputSystem.Player.SelectSphere.performed += OnSelectSphere;
        _inputSystem.Player.SelectCapsule.performed += OnSelectCapsule;
    }

    private void UnsubscribeEvents()
    {
        _inputSystem.Player.SelectCube.performed -= OnSelectCube;
        _inputSystem.Player.SelectSphere.performed -= OnSelectSphere;
        _inputSystem.Player.SelectCapsule.performed -= OnSelectCapsule;
    }

    private void Start()
    {
        SelectObject(0);
    }

    private void OnSelectCube(InputAction.CallbackContext context)
    {
        SelectObject(0);
    }

    private void OnSelectSphere(InputAction.CallbackContext context)
    {
        SelectObject(1);
    }

    private void OnSelectCapsule(InputAction.CallbackContext context)
    {
        SelectObject(2);
    }

    private void SelectObject(int index)
    {
        _cubeMover.enabled = (index == 0);
        _sphereMover.enabled = (index == 1);
        _capsuleMover.enabled = (index == 2);
    }
}