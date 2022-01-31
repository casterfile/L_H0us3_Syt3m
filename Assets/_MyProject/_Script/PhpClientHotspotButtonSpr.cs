using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PhpClientHotspotButtonSpr : MonoBehaviour
{
    public int __intCount;
    public GameObject PupupHotspot_popup;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetHotspotContent());
    }

    public IEnumerator GetHotspotContent()
    {
        UnityWebRequest www = UnityWebRequest.Get(Global_Variable.getHotspotView + "?HotspotTable_House_Hotspot_location_Count=" + __intCount);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);

        }
        else
        {
            // Show results as text
            //Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            //byte[] results = www.downloadHandler.data;

            string[] _urlTextQuery = www.downloadHandler.text.Split("|"[0]);
            string _strOutputAppend = "";
            int x = 0;
            foreach (string _textQuery in _urlTextQuery)
            {
                //print(_textQuery);
                //x++;
                //_strOutputAppend += _textQuery + "Num: "+x +"\n";
                /*string[] _newLine = _textQuery.Split("|"[0]);
                foreach (string _checkNewlind in _newLine)
                {
                    //_strOutputAppend += "\n";
                }*/
            }





            string DownloadUrl = Global_Variable.HostStreamingAsset + _urlTextQuery[11];
            StartCoroutine(DownloadImage(DownloadUrl));
        }
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
            //myTexture = new Texture(2, 2, TextureFormat.ARGB32, false);
            myTexture.filterMode = FilterMode.Point;
            myTexture.wrapMode = TextureWrapMode.Clamp;
            gameObject.GetComponent<Renderer>().material.mainTexture = myTexture;


            Sprite __HousePicture = Sprite.Create(((DownloadHandlerTexture)request.downloadHandler).texture, new Rect(0, 0, ((DownloadHandlerTexture)request.downloadHandler).texture.width, ((DownloadHandlerTexture)request.downloadHandler).texture.height), new Vector2(0, 0));
            
            gameObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(__HousePicture.texture, new Rect(0, 0, __HousePicture.texture.width, __HousePicture.texture.height), Vector2.one / 2,256);
        }

        print("Testing");
    }

    private void OnMouseDown()
    {
        PupupHotspot_popup.SetActive(true);
    }
}
