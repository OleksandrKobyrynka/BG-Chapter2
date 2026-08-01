using NaughtyAttributes;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string playerName;
    public int level;
    public float health;
}

public enum JsonConverter
{
    JsonUtility,
    NewtonsoftJson
}

public class JsonDemo : MonoBehaviour
{
    [SerializeField] private SaveData _data;

    [SerializeField] private string _fileName;

    [SerializeField] private JsonConverter _jsonConverterType = JsonConverter.JsonUtility;

    [Button]
    private void Save()
    {
        string path = Path.Combine(Application.streamingAssetsPath, $"{_fileName.Trim()}.json");
        SaveData data = new SaveData
        {
            playerName = _data.playerName,
            level = _data.level,
            health = _data.health
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

        SaveData data;
        string json = File.ReadAllText(path);
        if (_jsonConverterType == JsonConverter.JsonUtility)
        {
            data = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            data = JsonConvert.DeserializeObject<SaveData>(json);
        }

        Debug.Log($"Loaded: {data.playerName}, {data.level}, {data.health}");
    }
}
