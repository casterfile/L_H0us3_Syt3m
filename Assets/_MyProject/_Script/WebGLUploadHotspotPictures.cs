using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;


[RequireComponent(typeof(Button))]
public class WebGLUploadHotspotPictures : MonoBehaviour, IPointerDownHandler
{
    //,IPointerDownHandler
    public int __intIndex;
    public InputField __ifHotspotIcon, __ifHotsportPicture;
    public Image __imgHotspotIcon, __imgHotsportPicture;

    public Image __imgCurrentPhotos;
    public Sprite __sprDefualtImage;

    private string m_URL = Global_Variable.HostLocationPicture;
    private string _path;

    public void DefaultPicture()
    {
        __imgCurrentPhotos.sprite = __sprDefualtImage;
        __imgCurrentPhotos.preserveAspect = true;
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    public void OnPointerDown(PointerEventData eventData) {
        UploadFile(gameObject.name, "OnFileUploadHotspotPicture", ".jpg", false);
    }

    // Called from browser
    private void OnFileUploadHotspotPicture(string url) {
        string _url = url;
        UploadFile(_url, m_URL);
        print("Hotspot Icon");
         StartCoroutine(OutputRoutineHotspot(url));
        //__txtlog.text = "" + _url;
    }
#else
    //
    // Standalone platforms & editor
    //
    public void OnPointerDown(PointerEventData eventData) {}

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", "jpg", false);
        if (paths.Length > 0)
        {
            string _url = new System.Uri(paths[0]).AbsoluteUri;
            StartCoroutine(OutputRoutineHotspot(new System.Uri(paths[0]).AbsoluteUri));
            UploadFile(_url, m_URL);
            
        }
    }
#endif

    IEnumerator UploadFileCo(string localFileName, string uploadURL)
    {

        WWW localFile = new WWW(localFileName);//"file:///" + 
        yield return localFile;
        if (localFile.error == null)
            Debug.Log("Loaded file successfully");
        else
        {
            Debug.Log("Open file error: " + localFile.error);
            yield break; // stop the coroutine here
        }
        WWWForm postForm = new WWWForm();
        // version 1
        //postForm.AddBinaryData("theFile",localFile.bytes);
        // version 2
        print("localFileName: " + localFileName);

        postForm.AddBinaryData("theFile", localFile.bytes, localFileName, "text/plain");
        WWW upload = new WWW(uploadURL, postForm);
        yield return upload;
        if (upload.error == null)
        {
            Debug.Log("upload done :" + upload.text);
            string[] splitLocalFileName = localFileName.Split(char.Parse("/"));
            string lastString = splitLocalFileName[splitLocalFileName.Length-1];

#if UNITY_WEBGL && !UNITY_EDITOR
            //__ifHotspotIcon.text = "" + lastString+".jpg";
             if (__intIndex == 0)
            {
                __ifHotspotIcon.text = "" + lastString+".jpg";
            }
            else
            {
                __ifHotsportPicture.text = "" + lastString+".jpg";
            }
#else

            if (__intIndex == 0)
            {
                __ifHotspotIcon.text = "" + lastString;
            }
            else
            {
                __ifHotsportPicture.text = "" + lastString;
            }
#endif

            }
        else
        {
            Debug.Log("Error during upload: " + upload.error);
        }
            
    }
    void UploadFile(string localFileName, string uploadURL)
    {
        StartCoroutine(UploadFileCo(localFileName, uploadURL));

    }

    private IEnumerator OutputRoutineHotspot(string url)
    {
        var loader = new WWW(url);
        yield return loader;
        print("Picture View");
       if(__intIndex == 0)
        {
            __imgHotspotIcon.sprite = Sprite.Create(loader.texture, new Rect(0, 0, loader.texture.width, loader.texture.height), new Vector2(0, 0));
            __imgHotspotIcon.preserveAspect = true;
        }
        else
        {
            __imgHotsportPicture.sprite = Sprite.Create(loader.texture, new Rect(0, 0, loader.texture.width, loader.texture.height), new Vector2(0, 0));
            __imgHotsportPicture.preserveAspect = true;
        }

        __imgCurrentPhotos.sprite = Sprite.Create(loader.texture, new Rect(0, 0, loader.texture.width, loader.texture.height), new Vector2(0, 0));
        __imgCurrentPhotos.preserveAspect = true;

    }
}