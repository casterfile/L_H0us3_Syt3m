using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(Button))]
public class VideoController : MonoBehaviour
{
    public VideoPlayer playcontroller;
    private string url;

    public Text __txtLog;

    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(controller);
        playcontroller.Stop();
#if UNITY_WEBGL && !UNITY_EDITOR
        playcontroller.url = System.IO.Path.Combine(Application.streamingAssetsPath, "video.mp4");
        __txtLog.text = "" + System.IO.Path.Combine(Application.streamingAssetsPath, "video.mp4");
#else
        playcontroller.url = Global_Variable.HostStreamingAsset + "video.mp4";
        __txtLog.text = "" + Global_Variable.HostStreamingAsset + "video.mp4";
#endif


    }

    public void controller()
    {
        playcontroller.Play();
    }
}

#if UNITY_WEBGL && !UNITY_EDITOR

#else

#endif