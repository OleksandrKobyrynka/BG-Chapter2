using Unity.VisualScripting;
using UnityEngine;

public enum SphereCastMode
{
    SphereCast,
    SphereCastAll
}

public class SphereCastDemo : MonoBehaviour
{
    [SerializeField] private float _sphereRadius = 0.5f;
    [SerializeField] private SphereCastMode castMode = SphereCastMode.SphereCast;
    [SerializeField] private bool _isExecuted = false;

    private void Update()
    {
        HandleSphereCastMode();
    }

    private void HandleSphereCastMode()
    {
        if (!_isExecuted)
        {
            return;
        }

        if (castMode == SphereCastMode.SphereCast)
        {
            SphereCastDestroy();
        }

        else if (castMode == SphereCastMode.SphereCastAll)
        {
            SphereCastAllDestroy();
        }
    }

    private void SphereCastDestroy()
    {
        if (Physics.SphereCast(transform.position, _sphereRadius, Vector3.forward, out RaycastHit hit))
        {
            Debug.Log("SphereCast hit: " + hit.collider.name);
            Destroy(hit.collider.gameObject);
        }
    }

    private void SphereCastAllDestroy()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, _sphereRadius, Vector3.forward);
        foreach (var hit in hits)
        {
            Debug.Log("SphereCastAll hit: " + hit.collider.name);
            Destroy(hit.collider.gameObject);
        }
    }
}
