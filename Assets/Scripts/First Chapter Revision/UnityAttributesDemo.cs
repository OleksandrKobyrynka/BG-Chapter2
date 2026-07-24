using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

[DisallowMultipleComponent, RequireComponent(typeof(Collider)), SelectionBase]
//[ExecuteAlways]
[AddComponentMenu("Custom/Unity Attributes Demo")]
public class UnityAttributesDemo : MonoBehaviour
{
    [SerializeField, Min(0), FormerlySerializedAs("_privateFloat")] private float _privateFloatValue;

    [Space(10)]
    [SerializeField, TextArea(1, 5)] private string _privateString;

    [Space(10)]
    [SerializeField] private SerializedStruct _serializedStruct;

    [Space(10)]
    [SerializeField] private bool _isConsoleLogEnabled = false;

    [field: SerializeField, Space(10)] public int publicInt2 { get; private set; }

    [HideInInspector] public int publicInt;

    public float PrivateFloatProperty => _privateFloatValue;
    public string PrivateStringProperty => _privateString;

    private void Start()
    {
        ConditionalExample();
    }

    private void Update()
    {
        ExecuteAlwaysExample();
    }

    private void ExecuteAlwaysExample()
    {
        if (Application.isPlaying)
        {
            return;
        }
        else
        {
            if (_isConsoleLogEnabled)
            {
                Debug.Log("Update Method!");
            }
        }
    }

    [ContextMenu(nameof(ContextMenuExample))]
    private void ContextMenuExample()
    {
        Debug.Log("Context Menu Example!");
    }

    [Conditional("DEBUG")]
    private void ConditionalExample()
    {
        Debug.Log("Conditional Example!");
    }
}

[Serializable]
public struct SerializedStruct
{
    public int intValue;
    public float floatValue;
}
