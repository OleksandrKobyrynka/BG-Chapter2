using NaughtyAttributes;
using UnityEngine;

public class MovingObjectDemo : MonoBehaviour
{
    [SerializeField] private Vector3 _directionVector = new Vector3(0, 0, 1);
    [SerializeField] private float _step = 1f;
    [SerializeField] private bool _isMovingContinuously = false;

    private void Update()
    {
        if (_isMovingContinuously)
        {
            transform.position += _directionVector.normalized * _step * Time.deltaTime;
        }
    }

    [Button]
    private void MoveObject()
    {
        transform.position += _directionVector.normalized * _step;
    }
}
