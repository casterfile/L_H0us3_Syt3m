using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
public class DashboardMenu : MonoBehaviour
{
    [Space(5)]
    [Header("Windows Popup")]
    public GameObject __goWelcomeDashboard,__goCompanyProfileView, __goStatisticView, __goHotspotView;

    [Space(5)]
    [Header("Loading")]
    [SerializeField] private GameObject _goLoadingWindow, _LoadingImage;

    [Space(5)]
    [Header("Profile")]
    [SerializeField] 
    private InputField _ifCompany_Name, _ifCompany_ContactNumber, _ifCompany_Email, _ifCompany_Address;
    [Space(5)]
    [Header("Profile")]
    public TMP_InputField _ifCompany_Info;
    [Space(5)]
    [Header("Profile")]
    public Image _imgCompany_Picture;

    [Space(5)]
    [Header("Image Upload")]
    public Image __imgViewInfo, __imgViewInfoPictureUpload;
    private float _intViewInfoX, _intViewInfoY;
    private float _int__imgViewInfoPictureUploadX, _int__imgViewInfoPictureUploadY;

    private void Awake()
    {
        _intViewInfoX = __imgViewInfo.rectTransform.localPosition.x;
        _intViewInfoY = __imgViewInfo.rectTransform.localPosition.y;
        _int__imgViewInfoPictureUploadX = __imgViewInfoPictureUpload.rectTransform.localPosition.x;
        _int__imgViewInfoPictureUploadY = __imgViewInfoPictureUpload.rectTransform.localPosition.y;
    }

    // Start is called before the first frame update
    void Start()
    {

        HideAll();
        _goLoadingWindow.SetActive(true);
        __goWelcomeDashboard.SetActive(true);
        __imgViewInfo.rectTransform.localPosition = new Vector2(100000f, 100000f);
        __imgViewInfoPictureUpload.rectTransform.localPosition = new Vector2(10000f, 10000f);


        StartCoroutine(createCompanyAccount());
    }

    // Update is called once per frame
    void Update()
    {
        //Loading Image
        _LoadingImage.transform.Rotate(0, 0, -10, Space.Self);
    }


    public IEnumerator createCompanyAccount()
    {

        WWWForm form = new WWWForm();
        form.AddField("userinformation_ID", Global_Variable.User_ID);

        UnityWebRequest www = UnityWebRequest.Post(Global_Variable.createCompanyProfile, form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            //Debug.Log(www.downloadHandler.text);
            if (www.downloadHandler.text == "Invalid")
            {
                yield return new WaitForSeconds(1.5f);
                _goLoadingWindow.SetActive(false);
            }
            else
            {
                string[] _urlTextQuery = www.downloadHandler.text.Split("|"[0]);
                string _strOutputAppend = "";
                int x = 0;

                string CompanyProfileTable_Name = _urlTextQuery[1];
                string CompanyProfileTable_ContactNumber = _urlTextQuery[2];
                string CompanyProfileTable_Email = _urlTextQuery[3];
                string CompanyProfileTable_Address = _urlTextQuery[4];
                string CompanyProfileTable_Info = _urlTextQuery[5];
                string CompanyProfileTable_Picture = _urlTextQuery[6];

                _ifCompany_Name.text = CompanyProfileTable_Name;
                _ifCompany_ContactNumber.text = CompanyProfileTable_ContactNumber;
                _ifCompany_Email.text = CompanyProfileTable_Email;
                _ifCompany_Address.text = CompanyProfileTable_Address;
                _ifCompany_Info.text = CompanyProfileTable_Info;
                 
                StartCoroutine(OutputImage(CompanyProfileTable_Picture, _imgCompany_Picture));

                yield return new WaitForSeconds(1.5f);
                _goLoadingWindow.SetActive(false);

            }
        }
    }

    private IEnumerator OutputImage(string url, Image ImageOutput)
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

    private void HideAll()
    {
        __goWelcomeDashboard.SetActive(false);
        __goCompanyProfileView.SetActive(false);
        __goHotspotView.SetActive(false);
        __goStatisticView.SetActive(false);
        //__goViewInfo.SetActive(false);
    }

    public void ViewCompanyProfile()
    {
        HideAll();
        __goCompanyProfileView.SetActive(true);
    }

    public void ViewHotspot()
    {
        HideAll();
        __goHotspotView.SetActive(true);
        print("Close Hotspot view");
    }
    public void ViewStatistic()
    {
        HideAll();
        __goStatisticView.SetActive(true);
    }


    public void ViewHotspotInfo()
    {
        HideAll();
        __goHotspotView.SetActive(true);
        //__goViewInfo.SetActive(true);
    }

    public void CloseViewHotspotInfo()
    {
        __imgViewInfo.rectTransform.localPosition = new Vector2(100000f, 100000f);
    }

    public void CloseViewHotspotPictureUpload()
    {
        __imgViewInfoPictureUpload.rectTransform.localPosition = new Vector2(10000f, 10000f);
    }



}
