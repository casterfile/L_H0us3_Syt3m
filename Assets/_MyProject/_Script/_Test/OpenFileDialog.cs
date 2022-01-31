using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using SFB;
using System;

public class OpenFileDialog : MonoBehaviour
{
    private string m_URL = Global_Variable.HostLocationVideo;
    private string _path;
    public void OpenFileExplorer()
    {
        WriteResult(StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false));
        print("_path: " + _path);
        string _strConverString = _path;
        UploadFile(_strConverString, m_URL);
    }
    public void WriteResult(string[] paths)
    {
        if (paths.Length == 0)
        {
            return;
        }

        _path = "";
        foreach (var p in paths)
        {
            _path += p + "\n";
        }
    }

    public void WriteResult(string path)
    {
        _path = path;

        
    }


    


    IEnumerator UploadFileCo(string localFileName, string uploadURL)
    {
        WWW localFile = new WWW("file:///" + localFileName);
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