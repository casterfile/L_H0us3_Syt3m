using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class PhpClientGetHotspot : MonoBehaviour
{
    public float test;
    public int __intCount;
    //public InputField __ifTitle, __ifPicture, __ifVideo, __ifContent;
    public Text __HotspotTable_ID;
    public TMP_Text __HouseTitle;
    public VideoPlayer __HouseVideoPlayer;
    public YoutubePlayer.YTPlayer yPlayer = new YoutubePlayer.YTPlayer();
    public Image __HousePicture;
    public TMP_InputField __HouseContent;

    public Image __imgOriginalSize;
    public GameObject __goEnlargeOriginal;
    private float _fltOriginalSizeX, _fltOriginalSizeY;

    public Image __imgViewInfo;
    private float _fltViewImageX, _fltViewImageY;
    private string __strTitle, __strPicture, __strVideo, __strContent;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

        Camera maincamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector3 lookAtPosition = maincamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, maincamera.nearClipPlane));
        LeanTween.moveLocal(gameObject, lookAtPosition, 0.3f);

        //Setting Up Location
        _fltViewImageX = __imgViewInfo.rectTransform.localPosition.x;
        _fltViewImageY = __imgViewInfo.rectTransform.localPosition.y;

        _fltOriginalSizeX = __imgOriginalSize.rectTransform.localPosition.x;
        _fltOriginalSizeY = __imgOriginalSize.rectTransform.localPosition.y;
        __goEnlargeOriginal.SetActive(false);
        var button = GetComponent<Button>();
        SetContent();
        //button.onClick.AddListener(SetContent);

        //Hide After Setting
        //
    }
    private void Update()
    {
        
    }

    // Update is called once per frame
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


            /*__ifVideo.text = _urlTextQuery[4];
            __ifPicture.text = _urlTextQuery[5];*/
            
            __HouseVideoPlayer.prepareCompleted += VideoPlayerOnPrepareCompleted;
            yPlayer.youtubeUrl = _urlTextQuery[4];
            
            Prepare();
            


            string DownloadUrl = Global_Variable.HostStreamingAsset + _urlTextQuery[5];
            StartCoroutine(DownloadImage(DownloadUrl));
            __HouseTitle.text = _urlTextQuery[6];
            __HouseContent.text = _urlTextQuery[8];
            __HotspotTable_ID.text = _urlTextQuery[10];
            __imgViewInfo.rectTransform.localPosition = new Vector2(_fltViewImageX, _fltViewImageY);
        }
    }

    void SetContent()
    {
        __HouseTitle.text = "";
        __HouseContent.text = "";
        __HotspotTable_ID.text = "";
        StartCoroutine(GetHotspotContent());
    }

    //Download Video
    public async void Prepare()
    {
        Debug.Log("Loading video...");
        yPlayer.VideoPlayer = __HouseVideoPlayer;
        await yPlayer.PrepareVideoAsync();
        Debug.Log("Video ready");
    }

    void VideoPlayerOnPrepareCompleted(VideoPlayer source)
    {
        /*m_Button.interactable = videoPlayer.isPrepared;*/
        __HouseVideoPlayer.Play();
    }


    //Get Image
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

            __HousePicture.sprite = Sprite.Create(((DownloadHandlerTexture)request.downloadHandler).texture, new Rect(0, 0, ((DownloadHandlerTexture)request.downloadHandler).texture.width, ((DownloadHandlerTexture)request.downloadHandler).texture.height), new Vector2(0, 0));
            __HousePicture.preserveAspect = true;
        }

        print("Testing");
    }

    
    public void EnlargeVideo()
    {
        __HouseVideoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
        __HouseVideoPlayer.targetCamera = Camera.main;
        __goEnlargeOriginal.SetActive(true);
        __imgOriginalSize.rectTransform.localPosition = new Vector2(9999999.0f, 999999.0f);
    }

    public void EnlargeVideoOrigin()
    {
        __HouseVideoPlayer.renderMode = VideoRenderMode.RenderTexture;
        __goEnlargeOriginal.SetActive(false);
        __imgOriginalSize.rectTransform.localPosition = new Vector2(_fltOriginalSizeX, _fltOriginalSizeY);
    }

    public void CloseHotspot(GameObject hotspot)
    {
        hotspot.SetActive(false);
    }

}
