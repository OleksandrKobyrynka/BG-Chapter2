using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class UniTaskDemo : MonoBehaviour
{
    private CancellationToken _destroyToken;

    private void Awake()
    {
        _destroyToken = this.GetCancellationTokenOnDestroy();
    }

    private void Start()
    {
        WaitUntilExample().Forget();
    }

    [Button]
    private async UniTaskVoid DelayExample()
    {
        Debug.Log("Delay example");
        await UniTask.Delay(2000, cancellationToken: _destroyToken);
        Debug.Log("2 seconds have passed");
    }

    private async UniTaskVoid WaitUntilExample()
    {
        Debug.Log("WaitUntil example");
        await UniTask.WaitUntil(() => Keyboard.current.spaceKey.wasPressedThisFrame, cancellationToken: _destroyToken);
        Debug.Log("Space key was pressed");
    }

    [Button]
    private async UniTaskVoid DestroyTokenExample()
    {
        while (!_destroyToken.IsCancellationRequested)
        {
            await UniTask.Yield(cancellationToken: _destroyToken);
            Debug.Log("DestroyToken example");
        }
    }
}
