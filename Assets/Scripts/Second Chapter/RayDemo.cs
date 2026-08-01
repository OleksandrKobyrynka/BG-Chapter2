using UnityEngine;
using UnityEngine.InputSystem;

public class RayDemo : MonoBehaviour
{
    [SerializeField] private float _rayLength = 20f;
    [SerializeField] private string _rayTargetLayerName = "RayTargets";
    [SerializeField] private Transform _rayStart;

    private LineRenderer _pointerLine;
    private Camera _camera;
    private LayerMask _rayTargetLayer;

    private void Awake()
    {
        _pointerLine = GetComponent<LineRenderer>();
        _camera = GetComponent<Camera>();
        _rayTargetLayer = LayerMask.NameToLayer(_rayTargetLayerName);
    }

    private void LateUpdate()
    {
        HandleRay();
    }

    private void HandleRay()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            ActivateRay(Color.red);
        }

        else if (Mouse.current.rightButton.isPressed)
        {
            ActivateRay(Color.green);
        }

        else
        {
            _pointerLine.enabled = false;
        }
    }

    private void ActivateRay(Color hitColor)
    {
        _pointerLine.enabled = true;

        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Ray ray = _camera.ScreenPointToRay(mouseScreenPos);

        Vector3 rayTarget;

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _rayLength))
        {
            rayTarget = hitInfo.point;
            _pointerLine.SetPosition(0, _rayStart.position);
            _pointerLine.SetPosition(1, rayTarget);
            Debug.DrawRay(ray.origin, ray.direction * hitInfo.distance, Color.yellow);

            var objectHit = hitInfo.collider.gameObject;
            if (objectHit.layer == _rayTargetLayer)
            {
                objectHit.GetComponent<Renderer>().material.color = hitColor;
            }
        }
        else
        {
            rayTarget = ray.origin + ray.direction * _rayLength;
            _pointerLine.SetPosition(0, _rayStart.position);
            _pointerLine.SetPosition(1, rayTarget);
            Debug.DrawRay(ray.origin, ray.direction * _rayLength, Color.yellow);
        }
    }
}
