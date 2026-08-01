using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public enum AngleMode
{
    Euler,
    Quaternion
}

public class RotationInterpolationDemo : MonoBehaviour
{
    [Header("Mode")]
    [SerializeField] private AngleMode _mode = AngleMode.Euler;

    [Header("Rotation Settings")]
    [SerializeField] private float _rpm = 60f;
    [SerializeField] private Vector3 _rotationAxis = Vector3.up;
    [SerializeField] private bool _isRotationEnabled = false;

    [Header("Interpolation Settings")]
    [SerializeField] private Vector3 _startEuler = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 _targetEuler = new Vector3(0f, 359f, 0f);
    [SerializeField] private float _duration = 2f;

    private void Update()
    {
        if (_isRotationEnabled)
        {
            Rotation();
        }
    }

    private void Rotation()
    {
        float degreesPerSecond = _rpm * 6f;
        Vector3 normalizedAxis = _rotationAxis.normalized;

        if (_mode == AngleMode.Euler)
        {
            Vector3 eulerStep = normalizedAxis * degreesPerSecond * Time.deltaTime;
            transform.Rotate(eulerStep, Space.Self);
        }
        else
        {
            Quaternion deltaRotation = Quaternion.AngleAxis(degreesPerSecond * Time.deltaTime, normalizedAxis);
            transform.rotation *= deltaRotation;
        }
    }
    private IEnumerator Interpolation()
    {
        float elapsed = 0f;

        Quaternion startRotation = Quaternion.Euler(_startEuler);
        Quaternion targetRotation = Quaternion.Euler(_targetEuler);

        transform.rotation = startRotation;

        while (elapsed < _duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _duration);

            if (_mode == AngleMode.Euler)
            {
                Vector3 currentEuler = Vector3.Slerp(_startEuler, _targetEuler, t);
                transform.rotation = Quaternion.Euler(currentEuler);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            }

            yield return null;
        }

        transform.rotation = targetRotation;
    }

    [Button]
    private void PlayInterpolation()
    {
        StartCoroutine(Interpolation());
    }

    [Button]
    public void ResetToStart()
    {
        transform.rotation = Quaternion.identity;
    }
}