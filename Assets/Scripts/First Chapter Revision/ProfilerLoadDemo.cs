using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Profiling;

public class ProfilerLoadDemo : MonoBehaviour
{
    [SerializeField] private bool _simulateCpuLoad = false;
    [SerializeField] private bool _simulateGcAlloc = false;

    [SerializeField] private int _cpuIterations = 200000;
    [SerializeField] private int _allocationsPerFrame = 100;

    [SerializeField][Range(100, 1000)] private int _spawnCount = 300;
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Vector3 _spawnArea = new Vector3(10f, 5f, 10f);

    private readonly List<GameObject> _spawnedObjects = new();
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
            SpawnObjectsOnce();
        }

        if (Keyboard.current.vKey.wasPressedThisFrame)
        {
            ClearSpawnedObjects();
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

    private void SpawnObjectsOnce()
    {
        if (_cubePrefab == null)
        {
            Debug.LogWarning("Cube Prefab is not assigned.");
            return;
        }

        if (_spawnedObjects.Count > 0)
        {
            Debug.Log("Objects already spawned. Press V to clear them first.");
            return;
        }

        Profiler.BeginSample("Demo/Mass Spawn Objects");

        for (int i = 0; i < _spawnCount; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(-_spawnArea.x, _spawnArea.x),
                Random.Range(1f, _spawnArea.y),
                Random.Range(-_spawnArea.z, _spawnArea.z)
            );

            GameObject clone = Instantiate(_cubePrefab, position, Random.rotation);
            _spawnedObjects.Add(clone);
        }

        Profiler.EndSample();

        Debug.Log($"Spawned {_spawnCount} objects.");
    }

    private void ClearSpawnedObjects()
    {
        Profiler.BeginSample("Demo/Clear Spawned Objects");

        for (int i = 0; i < _spawnedObjects.Count; i++)
        {
            if (_spawnedObjects[i] != null)
            {
                Destroy(_spawnedObjects[i]);
            }
        }

        _spawnedObjects.Clear();

        Profiler.EndSample();

        Debug.Log("All spawned objects cleared.");
    }
}