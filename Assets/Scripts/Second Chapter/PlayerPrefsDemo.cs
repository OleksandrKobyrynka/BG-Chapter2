using NaughtyAttributes;
using UnityEngine;

public class PlayerPrefsDemo : MonoBehaviour
{
    [SerializeField] private string _intKey;
    [SerializeField] private int _intValue;

    private string Key => _intKey.Trim();

    [Button]
    private void SaveInt()
    {
        if (string.IsNullOrWhiteSpace(_intKey))
        {
            Debug.LogWarning("Key is empty");
            return;
        }

        PlayerPrefs.SetInt(Key, _intValue);
        PlayerPrefs.Save();
        Debug.Log($"Int saved: key: {Key}, value: {_intValue}");
    }

    [Button]
    private void LoadInt()
    {
        if (string.IsNullOrWhiteSpace(_intKey))
        {
            Debug.LogWarning("Key is empty");
            return;
        }

        int value = PlayerPrefs.GetInt(Key, 0);
        Debug.Log($"Int loaded: value: {value}");
    }

    [Button]
    private void DeleteInt()
    {
        if (string.IsNullOrWhiteSpace(_intKey))
        {
            Debug.LogWarning("Key is empty");
            return;
        }

        if (!PlayerPrefs.HasKey(Key))
        {
            Debug.Log("Key not found");
            return;
        }

        PlayerPrefs.DeleteKey(Key);
        PlayerPrefs.Save();
        Debug.Log("Deleted");
    }

    [Button]
    private void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}