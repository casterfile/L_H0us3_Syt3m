using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OpenURL : MonoBehaviour
{
    public string url = "http://unity3d.com/";
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(onclick);
    }
    private void onclick()
    {
        Application.OpenURL(url);
    }
}
