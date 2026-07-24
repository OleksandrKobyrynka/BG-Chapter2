using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Profiling;

public class ProfilerLoadDemo : MonoBehaviour
{
    [SerializeField] private bool _simulateCpuLoad = false;
    [SerializeField] private bool _simulateGcAlloc = false;
    [SerializeField] private bool _simulateObjectSpam = false;

    [SerializeField] private int _cpuIterations = 200000;
    [SerializeField] private int _allocationsPerFrame = 100;
    [SerializeField] private int _objectsPerBurst = 20;

    [SerializeField] private GameObject _cubePrefab;

    private float _value;

    private void Update()
    {
        HandleInput();

        if (_simulateCpuLoad)
        {
            SimulateCpuLoad();
        }

        if (_simulateGcAlloc)
        {
            SimulateGcAlloc();
        }

        if (_simulateObjectSpam)
        {
            SimulateObjectSpam();
        }
    }

    private void HandleInput()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            _simulateCpuLoad = !_simulateCpuLoad;
            Debug.Log($"CPU Load: {_simulateCpuLoad}");
        }

        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            _simulateGcAlloc = !_simulateGcAlloc;
            Debug.Log($"GC Alloc: {_simulateGcAlloc}");
        }

        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            _simulateObjectSpam = !_simulateObjectSpam;
            Debug.Log($"Object Spam: {_simulateObjectSpam}");
        }
    }

    private void SimulateCpuLoad()
    {
        Profiler.BeginSample("Demo/CPU Load");

        for (int i = 0; i < _cpuIterations; i++)
        {
            _value += Mathf.Sqrt(i) * Mathf.Sin(Time.time);
        }

        Profiler.EndSample();
    }

    private void SimulateGcAlloc()
    {
        Profiler.BeginSample("Demo/GC Alloc");

        for (int i = 0; i < _allocationsPerFrame; i++)
        {
            string text = Time.frameCount.ToString();
            int[] numbers = new int[10000];
            numbers[0] = text.Length;
        }

        Profiler.EndSample();
    }

    private void SimulateObjectSpam()
    {
        if (_cubePrefab == null)
        {
            return;
        }

        Profiler.BeginSample("Demo/Object Spam");

        for (int i = 0; i < _objectsPerBurst; i++)
        {
            GameObject clone = Instantiate(_cubePrefab, Random.insideUnitSphere * 3f, Quaternion.identity);
            Destroy(clone, 0.1f);
        }

        Profiler.EndSample();
    }
}