using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_Variable : MonoBehaviour 
{
    private const string HostingAddress = "https://livra3d.com/libraworld2v/";
    public const string HostLocationVideo = ""+ HostingAddress + "/Server/uploadVideo.php";
    public const string HostLocationPicture = "" + HostingAddress + "/Server/uploadPicture.php";
    public const string HostStreamingAsset = "" + HostingAddress + "/StreamingAssets/";
    
    //Hotspot
    public const string getHotspotView = "" + HostingAddress + "/Server/getHotspotView.php";
    public const string saveHotspotView = "" + HostingAddress + "/Server/saveHotspotView.php";


    //Clien Side
    public const string clientGetHotspotView = "" + HostingAddress + "/Server/clientGetHotspotView.php";

    //Dashboard
    public const string createCompanyProfile = "" + HostingAddress + "/Server/createCompanyProfile.php";

    //Login System
    public const string gRegistration = "" + HostingAddress + "/Server/gRegistration.php";
    public const string gLogin = "" + HostingAddress + "/Server/gLogin.php";
    public const string gPrivacyPolicy = HostingAddress+"/privacypolicy.html";
    public const string gSendEmailVerification = HostingAddress + "/Server/SendEmailVerification.php";
    public const string gSendForgetPassword = HostingAddress + "/Server/SendForgetPassword.php";


    //Account Variable
    public static string User_Admin = "0";
    public static string User_Banned = "0";
    public static string User_ID = "";


    public const string Dashboard_Admin_SalesPerson = "CSM_Scene01";
    public const string Dashboard_Admin_SuperAdmin = "CSM_Scene01";
    public const string Dashboard_Client = "HotspotClient";


    //General Variable
    public static string windowsName = "";
    public static bool isKeyboardOn = false;

}
