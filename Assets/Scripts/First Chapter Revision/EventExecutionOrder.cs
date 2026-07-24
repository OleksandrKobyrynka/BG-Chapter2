using UnityEngine;

public class EventSubscriber : MonoBehaviour
{
    [SerializeField] private EventPublisher publisher;

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        publisher.OnSomething += Method1;
        publisher.OnSomething += Method2;
        publisher.OnSomething += Method3;
    }

    private void UnsubscribeEvents()
    {
        publisher.OnSomething -= Method1;
        publisher.OnSomething -= Method2;
        publisher.OnSomething -= Method3;
    }

    private void Method1() => Debug.Log("Method 1");
    private void Method2() => Debug.Log("Method 2");
    private void Method3() => Debug.Log("Method 3");
}