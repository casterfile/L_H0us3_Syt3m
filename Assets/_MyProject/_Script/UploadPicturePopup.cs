using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UploadPicturePopup : MonoBehaviour
{
    public Image __imgViewInfoPictureUpload;
    public WebGLUploadHotspotPictures webglupload = new WebGLUploadHotspotPictures();
    private float _int__imgViewInfoPictureUploadX, _int__imgViewInfoPictureUploadY;
    //Start is called before the first frame update
    
    void Awake()
    {
        _int__imgViewInfoPictureUploadX = __imgViewInfoPictureUpload.rectTransform.localPosition.x;
        _int__imgViewInfoPictureUploadY = __imgViewInfoPictureUpload.rectTransform.localPosition.y;
    }

    // Update is called once per frame
    public void OnclickShow(int index)
    {
        webglupload.__intIndex = index;
        webglupload.DefaultPicture();
        __imgViewInfoPictureUpload.rectTransform.localPosition = new Vector2(_int__imgViewInfoPictureUploadX, _int__imgViewInfoPictureUploadY);
    }
}
