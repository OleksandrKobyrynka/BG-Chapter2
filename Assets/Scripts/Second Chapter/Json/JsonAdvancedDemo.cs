using NaughtyAttributes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class AdvancedSaveData
{
    public string playerName;
    public Dictionary<string, int> inventory = new();
    public int coins { get; set; }
}

public class JsonAdvancedDemo : MonoBehaviour
{
    [SerializeField] private string _fileName;

    [SerializeField] private JsonConverter _jsonConverterType = JsonConverter.JsonUtility;

    [Button]
    private void Save()
    {
        string path = Path.Combine(Application.streamingAssetsPath, $"{_fileName.Trim()}.json");
        AdvancedSaveData data = new AdvancedSaveData
        {
            playerName = "John",
            inventory = new Dictionary<string, int>
            {
                ["Apple"] = 5,
                ["Sword"] = 1,
                ["Health Potion"] = 2
            },
            coins = 5
        };

        string json;
        if (_jsonConverterType == JsonConverter.JsonUtility)
        {
            json = JsonUtility.ToJson(data, true);
        }
        else
        {
            json = JsonConvert.SerializeObject(data, Formatting.Indented);
        }

        File.WriteAllText(path, json);
        Debug.Log("Saved:\n" + json);
    }

    [Button]
    private void Load()
    {
        string path = Path.Combine(Application.streamingAssetsPath, $"{_fileName.Trim()}.json");
        if (!File.Exists(path))
        {
            Debug.LogWarning("File not found: " + path);
            return;
        }

        AdvancedSaveData data;
        string json = File.ReadAllText(path);
        if (_jsonConverterType == JsonConverter.JsonUtility)
        {
            data = JsonUtility.FromJson<AdvancedSaveData>(json);
        }
        else
        {
            data = JsonConvert.DeserializeObject<AdvancedSaveData>(json);
        }

        Debug.Log($"Name: {data.playerName}");
        if (data.inventory == null || data.inventory.Count == 0)
        {
            Debug.Log("Inventory is null or empty");
        }
        else
        {
            foreach (var item in data.inventory)
            {
                Debug.Log($"Item type: {item.Key}, count: {item.Value}");
            }
        }
        Debug.Log($"Coins: {data.coins}");
    }
}
