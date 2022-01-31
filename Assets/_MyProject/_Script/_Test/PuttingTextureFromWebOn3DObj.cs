using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PuttingTextureFromWebOn3DObj : MonoBehaviour
{
    public string __file = "3e67776e-1b12-4123-a1fa-e2a21aec3691.jpg";
    private string url = Global_Variable.HostStreamingAsset;
    void Start()
    {
        string DownloadUrl = url + __file;
        StartCoroutine(DownloadImage(DownloadUrl));
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log("Error" + request.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            gameObject.GetComponent<Renderer>().material.mainTexture = myTexture;
        }

        print("Testing");
    }
}
