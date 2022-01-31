using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(Button))]
public class PhpSaveHotspot : MonoBehaviour
{
    public Text __HotspotTable_ID;
    public InputField __ifIconHotspot,__ifTitle, __ifPicture, __ifVideo;
    public TMP_InputField __ifContent;
    public Image __imgViewInfo, __imgIconHospot, __imgPicture;
    private float _fltViewImageX, _fltViewImageY;
    private string __strTitle, __strPicture, __strVideo, __strContent;
    
    // Start is called before the first frame update
    void Start()
    {
        _fltViewImageX = __imgViewInfo.rectTransform.localPosition.x;
        _fltViewImageY = __imgViewInfo.rectTransform.localPosition.y;
        var button = GetComponent<Button>();
        
        button.onClick.AddListener(SaveContent);
    }


    // Update is called once per frame
    public IEnumerator GetHotspotContent()
    {
        WWWForm form = new WWWForm();
        form.AddField("HotspotTable_Text_Title", __ifTitle.text);
        form.AddField("HotspotTable_Picture", __ifPicture.text);
        form.AddField("HotspotTable_Video", __ifVideo.text);
        form.AddField("HotspotTable_Text_Content", __ifContent.text);
        form.AddField("HotspotTable_ID", __HotspotTable_ID.text);
        form.AddField("Hotspottable_Hotspot_Icon", __ifIconHotspot.text);

        UnityWebRequest www = UnityWebRequest.Post(Global_Variable.saveHotspotView, form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);
        }

    }

    void SaveContent()
    {
        StartCoroutine(GetHotspotContent());
    }
}
