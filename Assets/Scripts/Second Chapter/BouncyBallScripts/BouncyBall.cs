using TMPro;
using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bouncesCountText;
    [SerializeField] private TextMeshProUGUI _distanceText;
    [SerializeField] private string _layerName = "Floor";

    private int _bouncesCount = 0;
    private int _floorLayer;
    private Collider _collider;

    private void Awake()
    {
        _floorLayer = LayerMask.NameToLayer(_layerName);

        _collider = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        Vector3 rayOrigin = new Vector3(
                    _collider.bounds.center.x,
                    _collider.bounds.min.y,
                    _collider.bounds.center.z
                );

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity, 1 << _floorLayer))
        {
            _distanceText.text = "Distance: " + hitInfo.distance.ToString("F2");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == _floorLayer)
        {
            _bouncesCount++;
            _bouncesCountText.text = "Bounces: " + _bouncesCount;
        }
    }
}