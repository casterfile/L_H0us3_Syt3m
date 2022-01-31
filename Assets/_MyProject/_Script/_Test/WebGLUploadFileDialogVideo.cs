using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;

[RequireComponent(typeof(Button))]
public class WebGLUploadFileDialogVideo : MonoBehaviour, IPointerDownHandler
{
    public Text __txtlog;

    private string m_URL = Global_Variable.HostLocationVideo;
    private string _path;

#if UNITY_WEBGL && !UNITY_EDITOR
    //
    // WebGL
    //
    [DllImport("__Internal")]
    private static extern void UploadFile(string gameObjectName, string methodName, string filter, bool multiple);

    public void OnPointerDown(PointerEventData eventData) {
        UploadFile(gameObject.name, "OnFileUpload", ".mp4", false);
    }

    // Called from browser
    public void OnFileUpload(string url) {
        UploadFile(url, m_URL);
    }
#else
    //
    // Standalone platforms & editor
    //
    public void OnPointerDown(PointerEventData eventData) { }

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", "mp4", false);
        if (paths.Length > 0)
        {
            string _url = new System.Uri(paths[0]).AbsoluteUri;
            UploadFile(_url, m_URL);
            __txtlog.text = "OUTPUT FILE LOCATION: " + _url;
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
            Debug.Log("upload done :" + upload.text);
        else
            Debug.Log("Error during upload: " + upload.error);
    }
    void UploadFile(string localFileName, string uploadURL)
    {
        StartCoroutine(UploadFileCo(localFileName, uploadURL));
    }
}