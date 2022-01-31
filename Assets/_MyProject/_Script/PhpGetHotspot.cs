using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class PhpGetHotspot : MonoBehaviour
{
    public int __intCount;
    public InputField __ifIconHotspot, __ifTitle, __ifPicture, __ifVideo;
    public TMP_InputField __ifContent;
    public Text __HotspotTable_ID;

    public Image __imgViewInfo, __imgIconHospot, __imgPicture;
    private float _fltViewImageX, _fltViewImageY;
    private string __strTitle, __strPicture, __strVideo, __strContent;
    
    // Start is called before the first frame update
    void Start()
    {
        _fltViewImageX = __imgViewInfo.rectTransform.localPosition.x;
        _fltViewImageY = __imgViewInfo.rectTransform.localPosition.y;
        //print("X:" + _fltViewImageX + " Y: " + _fltViewImageY);
        var button = GetComponent<Button>();
        
        button.onClick.AddListener(SetContent);
    }


    // Update is called once per frame
    public IEnumerator GetHotspotContent()
    {
        UnityWebRequest www = UnityWebRequest.Get(Global_Variable.getHotspotView+"?HotspotTable_House_Hotspot_location_Count="+__intCount);
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

            __ifVideo.text = _urlTextQuery[4];
            __ifPicture.text = _urlTextQuery[5];
            __ifTitle.text = _urlTextQuery[6];
            __ifContent.text = _urlTextQuery[8];
            __HotspotTable_ID.text = _urlTextQuery[10];
            __ifIconHotspot.text = _urlTextQuery[11];
            StartCoroutine(OutputPictures(__ifPicture.text, __imgPicture));
            StartCoroutine(OutputHotspot(__ifIconHotspot.text, __imgIconHospot));
            __imgViewInfo.rectTransform.localPosition = new Vector2(_fltViewImageX, _fltViewImageY);
        }
    }

    void SetContent()
    {
        __ifContent.text = "";
        __ifPicture.text = "";
        __ifTitle.text = "";
        __ifVideo.text = "";
        __HotspotTable_ID.text = "";
        StartCoroutine(GetHotspotContent());
    }

    private IEnumerator OutputPictures(string url, Image ImageOutput)
    {
        if(url.Length > 0)
        {
            url = Global_Variable.HostStreamingAsset + url;
            var loader = new WWW(url);
            yield return loader;
            print("url: " + url);
            ImageOutput.sprite = Sprite.Create(loader.texture, new Rect(0, 0, loader.texture.width, loader.texture.height), new Vector2(0, 0));
            ImageOutput.preserveAspect = true;
        }
        
    }

    private IEnumerator OutputHotspot(string url, Image ImageOutput)
    {
        if (url.Length > 0)
        {
            url = Global_Variable.HostStreamingAsset + url;
            var loader = new WWW(url);
            yield return loader;
            print("url: " + url);
            ImageOutput.sprite = Sprite.Create(loader.texture, new Rect(0, 0, loader.texture.width, loader.texture.height), new Vector2(0, 0));
            ImageOutput.preserveAspect = true;
        }

    }
}
