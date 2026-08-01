using NaughtyAttributes;
using Newtonsoft.Json;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class PostModel
{
    public int userId;
    public int id;
    public string title;
    public string body;
}

[System.Serializable]
public class CreatePostRequest
{
    public int userId;
    public string title;
    public string body;
}

public class UnityWebRequestDemo : MonoBehaviour
{
    private const string GET_URL = "https://jsonplaceholder.typicode.com/posts/1";
    private const string POST_URL = "https://jsonplaceholder.typicode.com/posts";

    [SerializeField] private CreatePostRequest _request;

    [Button]
    private void GetButton()
    {
        StartCoroutine(GetExample());
    }

    private IEnumerator GetExample()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(GET_URL))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("GET Error: " + request.error);
                yield break;
            }

            string json = request.downloadHandler.text;
            Debug.Log("GET Raw JSON:\n" + json);

            PostModel post = JsonConvert.DeserializeObject<PostModel>(json);
            Debug.Log($"GET Parsed -> id: {post.id}, title: {post.title}, body: {post.body}");
        }
    }


    [Button]
    private void PostButton()
    {
        StartCoroutine(PostExample());
    }

    private IEnumerator PostExample()
    {
        CreatePostRequest newPost = _request;

        string json = JsonConvert.SerializeObject(newPost);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest request = new UnityWebRequest(POST_URL, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("POST Error: " + request.error);
                yield break;
            }

            string responseJson = request.downloadHandler.text;
            Debug.Log("POST Response JSON:\n" + responseJson);

            PostModel createdPost = JsonConvert.DeserializeObject<PostModel>(responseJson);
            Debug.Log($"POST Parsed -> id: {createdPost.id}, title: {createdPost.title}, body: {createdPost.body}");
        }
    }
}
