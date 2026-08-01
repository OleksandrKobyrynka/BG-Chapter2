using NaughtyAttributes;
using UnityEngine;

public class AwaitableDemo : MonoBehaviour
{
    private async Awaitable Start()
    {
        Debug.Log("Before wait");
        await Awaitable.NextFrameAsync();
        Debug.Log("After next frame");
    }

    [Button]
    private async Awaitable RunAsync()
    {
        Debug.Log("Start");
        await Awaitable.WaitForSecondsAsync(2f);
        Debug.Log("2 seconds later");
    }

    [Button]
    private async Awaitable HeavyWorkAsync()
    {
        Debug.Log("Heavy work started");
        await Awaitable.BackgroundThreadAsync();

        long sum = 0;
        for (int i = int.MinValue; i < int.MaxValue; i++)
        {
            sum += i;
        }

        await Awaitable.MainThreadAsync();
        Debug.Log("Back on main thread" + sum);
    }

}
