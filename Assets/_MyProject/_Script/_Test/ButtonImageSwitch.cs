using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonImageSwitch : MonoBehaviour
{
    public Sprite Show, Hide;
    bool isShow = false;
    Image buttonImage;
    private void Start()
    {
        buttonImage = gameObject.GetComponent<Image>();
        buttonImage.sprite = Hide;
        isShow = false;
    }
    public void ButtonSwitch()
    {
        
        if(isShow == false)
        {
            isShow = true;
            buttonImage.sprite = Show;
        }
        else
        {
            isShow = false;
            buttonImage.sprite = Hide;
        }
    }

}
