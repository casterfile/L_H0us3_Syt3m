using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class ViewPicture : MonoBehaviour
{
    public string __file = "picture.jpg";
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
            Debug.Log("Error"+request.error);
        }
        else
        {
            Image image = GetComponent<Image>();
            image.sprite = Sprite.Create(((DownloadHandlerTexture)request.downloadHandler).texture, new Rect(0, 0, ((DownloadHandlerTexture)request.downloadHandler).texture.width, ((DownloadHandlerTexture)request.downloadHandler).texture.height), new Vector2(0, 0));
            image.preserveAspect = true;
        }

        print("Testing");
    }
}
