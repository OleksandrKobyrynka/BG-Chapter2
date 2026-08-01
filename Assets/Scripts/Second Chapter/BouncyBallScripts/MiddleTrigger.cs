using TMPro;
using UnityEngine;

public class MiddleTrigger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _passingsCountText;
    [SerializeField] private string _layerName = "Ball";

    private int _passingsCount = 0;
    private int _ballLayer;

    private void Awake()
    {
        _ballLayer = LayerMask.NameToLayer(_layerName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _ballLayer)
        {
            _passingsCount++;
            _passingsCountText.text = "Passings: " + _passingsCount;
        }
    }
}
