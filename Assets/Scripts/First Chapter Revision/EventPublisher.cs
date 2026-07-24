using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventPublisher : MonoBehaviour
{
    public event Action OnSomething;

    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            Raise();
        }
    }

    public void Raise()
    {
        Debug.Log("Publisher raised event");
        OnSomething?.Invoke();
    }
}