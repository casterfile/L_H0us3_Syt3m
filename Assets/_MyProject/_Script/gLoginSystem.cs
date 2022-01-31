using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class gLoginSystem : MonoBehaviour
{
    public GameObject __goLoginWindow, __goRegistrationWindow, __goTempRegistrationWindow, __goForgetPasswordWindow;
    public GameObject ___gobtnRegister,__RegistrationConfirmation, TooltipPassword;



    public bool ConsumeResources = true;
    [Space(5)]
    [Header("Registration")]
    public InputField  __ifPassword, __ifPasswordRetype, __ifFname, __ifLname, __ifPhone;
    [Space(5)]
    [Header("Registration")]
    public Text __ifUsername, __ifEmail;
    [Space(5)]
    [Header("Registration")]
    public Dropdown __dpGender, __dpBday_day, __dpBday_month, __dpBday_year;
    [Space(5)]
    public Text __txtRegisterOutput_log;

    [Space(5)]
    [Header("Login")]
    public InputField __ifLoginUsername, __ifLoginPassword;
    [Space(5)]
    [Header("Login")]
    public Text __txtLoginOutput_log;

    [Space(5)]
    [Header("TempRegistration")]
    public InputField __ifTempUsername, __ifTempEmail, __ifTempRetypeEmail;
    [Space(5)]
    [Header("TempRegistration")]
    public Text __txtTempRegisterOutput_log;

    [Space(5)]
    [Header("ForgetPassword")]
    public InputField __ifForgetPasswordUsername, __ifForgetPasswordEmail;
    [Space(5)]
    [Header("ForgetPassword")]
    public Text __txtForgetPasswordOutput_log;


    private const string  _ServerRegisterURL = Global_Variable.gRegistration;
    private const string _ServerLoginURL = Global_Variable.gLogin;

    private void Awake()
    {
        //Check for EmailVerification
        
    }

    public void URLCheckForFinalRegistration()
    {
        
        int pm = Application.absoluteURL.IndexOf("?");
        if (pm != -1)
        {
            string[] URLString = Application.absoluteURL.Split("?"[0]); //?*!-[+]-!*&
            string Email = URLString[1];
            string Username = URLString[2];
            string FunctionType = URLString[3];

            if(FunctionType == "FinalRegistration")
            {
                __ifUsername.text = Username;
                __ifEmail.text = Email;

                MoveXToLeft(__goLoginWindow);
                SetDialogCenter(__goRegistrationWindow);
            }
            
        }
        /*string checkTest = "https://livra3d.com/libraworld2v/?*!-[+]-!*&dev.anthonycastor@gmail.com?*!-[+]-!*&jason15x?*!-[+]-!*&FinalRegistration";
        string[] testData = checkTest.Split("?"[0]); //*!-[+]-!*&dev.anthonycastor@gmail.com
        print("testData: " + testData[1].Trim());*/
    }

    // Start is called before the first frame update
    void Start()
    {
        //Screen.fullScreen = !Screen.fullScreen;
        //SetDialogLocation(__goRegistrationWindow); __goLoginWindow, __goRegistrationWindow, __goTempRegistrationWindow;
        ___gobtnRegister.SetActive(false);
        __RegistrationConfirmation.SetActive(false);
        TooltipPassword.SetActive(false);

        //Windows
        __goLoginWindow.SetActive(true);
        __goRegistrationWindow.SetActive(true);
        __goTempRegistrationWindow.SetActive(true);
        __goForgetPasswordWindow.SetActive(true);

        __goRegistrationWindow.transform.localPosition = new Vector3(-2100f, 30f, 0f); 
        __goTempRegistrationWindow.transform.localPosition = new Vector3(-2100f, 30f, 0f);
        __goForgetPasswordWindow.transform.localPosition = new Vector3(-2100f, 30f, 0f);

        SetDialogCenter(__goLoginWindow);

        __goRegistrationWindow.SetActive(false);
        __goTempRegistrationWindow.SetActive(false);
        __goForgetPasswordWindow.SetActive(false);

        DropdownConfig();
        __txtRegisterOutput_log.text = "";
        __txtLoginOutput_log.text = "";
        __txtTempRegisterOutput_log.text = "";
        __txtForgetPasswordOutput_log.text = "";
        URLCheckForFinalRegistration();


        
    }

    // Update is called once per frame
    /*void Update()
    {
       
    }*/


    //Login
    public IEnumerator ForgetPasswordNow()
    {

        WWWForm form = new WWWForm();
        form.AddField("userinformation_username", __ifForgetPasswordUsername.text);
        //form.AddField("userinformation_email", __ifForgetPasswordEmail.text);

        UnityWebRequest www = UnityWebRequest.Post(Global_Variable.gSendForgetPassword, form);
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
                __txtForgetPasswordOutput_log.text = "Username or Email Invalid!";
                __txtForgetPasswordOutput_log.color = Color.red;
            }
            else
            {
                __txtForgetPasswordOutput_log.text = "Check your Email!";
                __txtForgetPasswordOutput_log.color = Color.green;
            }
        }
    }

    public void ClickForgetPasswordNow()
    {
        StartCoroutine(ForgetPasswordNow());
    }


    //Login
    public IEnumerator LoginNow()
    {

        WWWForm form = new WWWForm();
        form.AddField("userinformation_username", __ifLoginUsername.text);
        form.AddField("userinformation_password", __ifLoginPassword.text);

        UnityWebRequest www = UnityWebRequest.Post(_ServerLoginURL, form);
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
                __txtLoginOutput_log.text = "username or password Invalid";
                __txtLoginOutput_log.color = Color.red;
            }
            else
            {
                string[] _urlTextQuery = www.downloadHandler.text.Split("|"[0]);
                string _strOutputAppend = "";
                int x = 0;

                string User_Usernanme = _urlTextQuery[0];
                string User_fname = _urlTextQuery[2];
                string User_lname = _urlTextQuery[3];
                string User_bday = _urlTextQuery[4];
                string User_mday = _urlTextQuery[5];
                string User_yday = _urlTextQuery[6];
                string User_gender = _urlTextQuery[7];
                string User_address = _urlTextQuery[8];
                string User_email = _urlTextQuery[9];
                string User_telephone = _urlTextQuery[10];


                Global_Variable.User_Admin = _urlTextQuery[15];
                Global_Variable.User_Banned = _urlTextQuery[16]; 
                Global_Variable.User_ID = _urlTextQuery[17]; 
                string userinformation_activated = _urlTextQuery[18];
                string userinformation_forgetpassword = _urlTextQuery[19];

                print("Usernanme: " + User_Usernanme);
                print("Fname: " + User_fname);
                print("Lname: " + User_lname);
                print("Bday: " + User_bday);
                print("Mday: " + User_mday);
                print("Yday: " + User_yday);
                print("Gender: " + User_gender);
                print("Address: " + User_address);
                print("Email: " + User_email);
                print("Telephone: " + User_telephone);

                print("User_Admin: " + Global_Variable.User_Admin);
                print("User_Banned: " + Global_Variable.User_Banned);
                print("USer_ID: " + Global_Variable.User_ID);

                yield return new WaitForSeconds(3);
                __txtLoginOutput_log.text = "Successfully Login";
                __txtLoginOutput_log.color = Color.green;

                if (userinformation_activated != "2")
                {
                    __txtLoginOutput_log.text = "Inactive Account. Check your Email!!!";
                    __txtLoginOutput_log.color = Color.red;
                }
                else if (Global_Variable.User_Banned == "0")
                {
                    if (Global_Variable.User_Admin == "1")
                    {
                        SceneManager.LoadScene(Global_Variable.Dashboard_Admin_SalesPerson);
                    }
                    else if (Global_Variable.User_Admin == "2")
                    {
                        SceneManager.LoadScene(Global_Variable.Dashboard_Admin_SuperAdmin);
                    }
                    else if (Global_Variable.User_Admin == "0")
                    {
                        SceneManager.LoadScene(Global_Variable.Dashboard_Client);
                    }
                }
                else
                {
                    __txtLoginOutput_log.text = "Your Account is Banned!";
                }
                

            }
        }
    }

    public void ClickLoginNow()
    {
        StartCoroutine(LoginNow());
    }

    //Registration
    public IEnumerator SaveRegistration(int phpActivation)
    {
        WWWForm form = new WWWForm();
        form.AddField("userinformation_username", __ifUsername.text);

        //convert MD5
        //print("MD5 Converter: " + MD5HashConverter("**77Goddess77**" + __ifPassword.text + "!!88Master88!!"));
        string passwordData = MD5HashConverter("**77Goddess77**" + __ifPassword.text + "!!88Master88!!");
        form.AddField("userinformation_password", passwordData);
        form.AddField("userinformation_fname", __ifFname.text);
        form.AddField("userinformation_lname", __ifLname.text);
        form.AddField("userinformation_telephone", __ifPhone.text);
        form.AddField("userinformation_email", __ifEmail.text);

        //Convert value to String
        string Bday_day = __dpBday_day.options[__dpBday_day.value].text;
        string Bday_month = __dpBday_month.options[__dpBday_month.value].text;
        string Bday_year = __dpBday_year.options[__dpBday_year.value].text;
        string gender = __dpGender.options[__dpGender.value].text;


        form.AddField("userinformation_bday_day", Bday_day); 
        form.AddField("userinformation_bday_month", Bday_month);
        form.AddField("userinformation_bday_year", Bday_year);
        form.AddField("userinformation_gender", gender);
        form.AddField("userinformation_activated", phpActivation);

        UnityWebRequest www = UnityWebRequest.Post(_ServerRegisterURL, form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);
            if(www.downloadHandler.text == "Done")
            {
                __RegistrationConfirmation.SetActive(true);
                __txtRegisterOutput_log.text = "Successfully Registered";
                __txtRegisterOutput_log.color = Color.green;
                if (phpActivation == 1)
                {
                    //__RegistrationConfirmation.SetActive(false);
                    __txtRegisterOutput_log.text = "Successfully Registered";
                    __txtRegisterOutput_log.color = Color.green;

                    Text Info2 = __RegistrationConfirmation.gameObject.transform.GetChild(0).GetChild(8).gameObject.GetComponent<Text>();
                    Info2.text = "Successfully Registered! Redirecting to Login Page";
                    Info2.color = Color.green;
                    //Successful Register
                    //Go Login

                    yield return new WaitForSeconds(3.5f);
                    SetDialogLocation(__goRegistrationWindow);
                    SetDialogCenter(__goLoginWindow);
                    //Reset Data
                    //__ifUsername.text = __ifPassword.text = __ifPasswordRetype.text = __ifFname.text = __ifLname.text = __ifPhone.text = __ifEmail.text = "";
                    //__dpBday_day.value = __dpBday_month.value = __dpBday_year.value = __dpGender.value = 0;
                }
                
                
                
                Text confirmation_Username = __RegistrationConfirmation.gameObject.transform.GetChild(0).GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
                confirmation_Username.text = __ifUsername.text;
                Text confirmation_Fullname1 = __RegistrationConfirmation.gameObject.transform.GetChild(0).GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
                confirmation_Fullname1.text = __ifFname.text;
                Text confirmation_Fullname2 = __RegistrationConfirmation.gameObject.transform.GetChild(0).GetChild(3).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
                confirmation_Fullname2.text = __ifLname.text;
                Text confirmation_Gender = __RegistrationConfirmation.gameObject.transform.GetChild(0).GetChild(4).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
                confirmation_Gender.text = __dpGender.options[__dpGender.value].text;
                Text confirmation_Year = __RegistrationConfirmation.gameObject.transform.GetChild(0).GetChild(5).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
                confirmation_Year.text = __dpBday_year.options[__dpBday_year.value].text+ "年";
                Text confirmation_Month = __RegistrationConfirmation.gameObject.transform.GetChild(0).GetChild(5).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
                confirmation_Month.text = __dpBday_month.options[__dpBday_month.value].text + "月";
                Text confirmation_Day = __RegistrationConfirmation.gameObject.transform.GetChild(0).GetChild(5).gameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
                confirmation_Day.text = __dpBday_day.options[__dpBday_day.value].text + "日";
                Text confirmation_Email = __RegistrationConfirmation.gameObject.transform.GetChild(0).GetChild(6).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
                confirmation_Email.text = __ifEmail.text;
            }
            else
            {
                __txtRegisterOutput_log.text = "Username or E-Mail already exists";
                __txtRegisterOutput_log.color = Color.red;
            }
        }

    }

    public void ClickSaveRegistration(int phpActivation)
    {
        bool isUsername,isPassword,isFirstName,isLastName,isGender,isBday_Day, isBday_Month, isBday_Year, isEmail,isPhoneNumber;
        isUsername = isPassword = isFirstName = isLastName = isGender = isBday_Day = isBday_Month = isBday_Year = isEmail = isPhoneNumber = false;
        int usernamepasswordLength = 5;


        //Check Date If Number
        int number;

        if (int.TryParse(__dpBday_day.options[__dpBday_day.value].text, out number))
        {
            isBday_Day = true;
        }
        else
        {
            __txtRegisterOutput_log.text = "Pick a Birthday Day";
            __txtRegisterOutput_log.color = Color.red;
        }

        if (int.TryParse(__dpBday_month.options[__dpBday_month.value].text, out number))
        {
            isBday_Month = true;
        }
        else
        {
            __txtRegisterOutput_log.text = "Pick a Birthday Month";
            __txtRegisterOutput_log.color = Color.red;
        }

        if (int.TryParse(__dpBday_year.options[__dpBday_year.value].text, out number))
        {
            isBday_Year = true;
        }
        else
        {
            __txtRegisterOutput_log.text = "Pick a Birthday Year";
            __txtRegisterOutput_log.color = Color.red;
        }

        if (validateEmail(__ifEmail.text))
        {
            isEmail = true;
        }
        else
        {
            __txtRegisterOutput_log.text = "Invalid Email";
            __txtRegisterOutput_log.color = Color.red;
        }

        /*if (IsValidPhoneNumber(__ifPhone.text))
        {
            isPhoneNumber = true;
        }
        else
        {
            __txtRegisterOutput_log.text = "Not Valid Japanese Phone Number(+81120974259)";//+81 120-974-259
            __txtRegisterOutput_log.color = Color.red;
        }*/

        if (__dpGender.options[__dpGender.value].text != "Gender")
        {
            isGender = true;
        }
        else
        {
            __txtRegisterOutput_log.text = "Please Choose A Gender";
            __txtRegisterOutput_log.color = Color.red;
        }

        if (__ifLname.text.Length > 1)
        {
            isLastName = true;
        }
        else
        {
            __txtRegisterOutput_log.text = "Please Input Your Last Name";
            __txtRegisterOutput_log.color = Color.red;
        }


        if (__ifFname.text.Length > 1)
        {
            isFirstName = true;
        }
        else
        {
            __txtRegisterOutput_log.text = "Please Input Your First Name";
            __txtRegisterOutput_log.color = Color.red;
        }

        if (__ifPassword.text.Length > usernamepasswordLength)
        {
            if(__ifPasswordRetype.text == __ifPassword.text)
            {
                if(ValidatePassword(__ifPassword.text, __txtRegisterOutput_log))
                {
                    isPassword = true;
                }
                
            }
            else
            {
                __txtRegisterOutput_log.text = "Password Does Not Match";
                __txtRegisterOutput_log.color = Color.red;
            }
            
        }
        else
        {
            __txtRegisterOutput_log.text = "Password is too short";
            __txtRegisterOutput_log.color = Color.red;
        }

        

        if (__ifUsername.text.Length > usernamepasswordLength)
        {
            isUsername = true;
        }
        else
        {
            __txtRegisterOutput_log.text = "Username is too short";
            __txtRegisterOutput_log.color = Color.red;
        }

        

        if (isUsername == true && isPassword == true && isFirstName == true && isLastName == true && isGender == true && isEmail == true //&& isPhoneNumber == false
            && isBday_Day == true && isBday_Month == true && isBday_Month == true)
        {
            StartCoroutine(SaveRegistration(phpActivation));
        }
        
    }


    //Registration
    public IEnumerator SaveTempRegistration()
    {
        WWWForm form = new WWWForm();
        form.AddField("userinformation_username", __ifTempUsername.text);
        form.AddField("userinformation_email", __ifTempEmail.text);

        UnityWebRequest www = UnityWebRequest.Post(Global_Variable.gSendEmailVerification, form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
            Debug.Log(www.downloadHandler.text);
            if (www.downloadHandler.text == "Done")
            {
                __txtTempRegisterOutput_log.text = "Registration Send to you E-Mail.";
                __txtTempRegisterOutput_log.color = Color.green;
                //Successful Temp Register
                //Go Login
                __ifTempUsername.DeactivateInputField();
                __ifTempEmail.DeactivateInputField();
                __ifTempRetypeEmail.DeactivateInputField();
                yield return new WaitForSeconds(2.5f);
                SetDialogLocation(__goTempRegistrationWindow);
                SetDialogCenter(__goLoginWindow);
                //Reset Data
                __ifTempUsername.text = __ifTempEmail.text = __ifTempRetypeEmail.text = "";

                __ifTempUsername.ActivateInputField();
                __ifTempEmail.ActivateInputField();
                __ifTempRetypeEmail.ActivateInputField();
            }
            else
            {
                __txtTempRegisterOutput_log.text = "Username or E-Mail already exists";
                __txtTempRegisterOutput_log.color = Color.red;
            }
        }

    }


    public void ClickSaveTempRegistration()
    {
        bool isUsername,  isEmail;
        isUsername = isEmail  = false;
        int usernamepasswordLength = 5;


        if(__ifTempEmail.text == __ifTempRetypeEmail.text)
        {
            if (validateEmail(__ifTempEmail.text))
            {
                isEmail = true;
            }
            else
            {
                __txtTempRegisterOutput_log.text = "Invalid Email";
                __txtTempRegisterOutput_log.color = Color.red;
            }
        }
        else
        {
            __txtTempRegisterOutput_log.text = "Email Don't Match";
            __txtTempRegisterOutput_log.color = Color.red;
        }
        


        if (__ifTempUsername.text.Length > usernamepasswordLength)
        {
            isUsername = true;
        }
        else
        {
            __txtTempRegisterOutput_log.text = "Username is too short";
            __txtTempRegisterOutput_log.color = Color.red;
        }




        if (isUsername == true && isEmail == true)
        {
            StartCoroutine(SaveTempRegistration());
        }

    }






    public void MoveXToLeft(GameObject _goWindows)
    {
        //LeanTween.moveX(_goWindows, -2000, 0.3f);
        LeanTween.moveLocal(_goWindows,new Vector3(2100f,30f,0f), 0.3f);
        StartCoroutine(HideGameObject(_goWindows));
    }

    public void SetDialogLocation(GameObject _goWindows)
    {
        //LeanTween.moveX(_goWindows, 20000, 0.3f);
        LeanTween.moveLocal(_goWindows, new Vector3(-2100f, 30f, 0f), 0.3f);
        StartCoroutine(HideGameObject(_goWindows));
    }

    public void SetDialogCenter(GameObject _goWindows)
    {
        //LeanTween.moveX(_goWindows, 1300, 0.3f);
        _goWindows.SetActive(true);
        Camera maincamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector3 lookAtPosition = maincamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, maincamera.nearClipPlane));
        LeanTween.moveLocal(_goWindows, lookAtPosition, 0.3f);
        print("WindowsName:"+_goWindows.tag);
        Global_Variable.windowsName = _goWindows.tag;
    }


    private IEnumerator HideGameObject(GameObject _goWindows)
    {
        yield return new WaitForSeconds(0.5f);
        _goWindows.SetActive(false);
    }

    //Dropdown Configuration
    public void DropdownConfig()
    {
        List<string> Days = new List<string> { "Option 1", "Option 2" };
        Days.Clear();
        Days.Add("DAY");
        //Days.Add("Days");
        for (int x = 1; x < 32; x++)
        {
            Days.Add("" + x);
        }

        __dpBday_day.ClearOptions();
        __dpBday_day.AddOptions(Days);


        List<string> Months = new List<string> { "Option 1", "Option 2" };
        Months.Clear();
        Months.Add("MONTH");
        //Months.Add("Months");
        for (int x = 1; x < 13; x++)
        {
            Months.Add("" + x);
        }

        __dpBday_month.ClearOptions();
        __dpBday_month.AddOptions(Months);

        string data = System.DateTime.Now.ToString("yyyy");
        int YearValue = Convert.ToInt32(data);
        //YearValue = YearValue - 17;

        List<string> Years = new List<string> { "Option 1", "Option 2" };
        Years.Clear();
        Years.Add("YEAR");
        for (int x = 0; x < 60; x++)
        {
            int YearFinal = YearValue - x;
            Years.Add("" + YearFinal);
        }

        __dpBday_year.ClearOptions();
        __dpBday_year.AddOptions(Years);
    }


    //Email validation
    public const string MatchEmailPattern =
         @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
              + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

    public static bool validateEmail(string email)
    {
        if (email != null)
            return Regex.IsMatch(email, MatchEmailPattern);
        else
            return false;
    }


    //Validate Japan Phone Number
    private const string pattern = @"/^\(?(\+81|0)([-\d \(\)]{9,12})$/i";//@"^(\+83[0-9]{13})$" +819123456789
    public static bool IsValidPhoneNumber(string number)
    {
        //+81123456789 +819151594040
        return Regex.Match(number, @"^(\+81[0-9]{10})$").Success;
    }


    //MD5 Converter
    public static string MD5HashConverter(string input)
    {
        StringBuilder hash = new StringBuilder();
        MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
        byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

        for (int i = 0; i < bytes.Length; i++)
        {
            hash.Append(bytes[i].ToString("x2"));
        }
        return hash.ToString();
    }

    //Password validation Checker
    private bool ValidatePassword(string password, Text txtErrorMessage)
    {
        var input = password;
        string ErrorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(input))
        {
            throw new Exception("Password should not be empty");
        }

        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasMiniMaxChars = new Regex(@".{8,15}");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        if (!hasLowerChar.IsMatch(input))
        {
            ErrorMessage = "Password should contain at least one lower case letter.";
            txtErrorMessage.text = ErrorMessage;
            return false;
        }
        else if (!hasUpperChar.IsMatch(input))
        {
            ErrorMessage = "Password should contain at least one upper case letter.";
            txtErrorMessage.text = ErrorMessage;
            return false;
        }
        else if (!hasMiniMaxChars.IsMatch(input))
        {
            ErrorMessage = "Password should not be lesser than 8 or greater than 15 characters.";
            txtErrorMessage.text = ErrorMessage;
            return false;
        }
        else if (!hasNumber.IsMatch(input))
        {
            ErrorMessage = "Password should contain at least one numeric value.";
            txtErrorMessage.text = ErrorMessage;
            return false;
        }

        else if (!hasSymbols.IsMatch(input))
        {
            ErrorMessage = "Password should contain at least one special case character.";
            txtErrorMessage.text = ErrorMessage;
            return false;
        }
        else
        {
            return true;
        }
    }

    public void onTogglePrivacyPolicy(Toggle privacypolicy)
    {
        if (privacypolicy.isOn)
        {
            ___gobtnRegister.SetActive(true);
        }
        else
        {
            ___gobtnRegister.SetActive(false);
        }
    }




    public void onClickPirvacyPolicyAgreement()
    {
        Application.OpenURL(Global_Variable.gPrivacyPolicy);
    }

    
    

    public void onHideObject(GameObject HideObject)
    {
        HideObject.SetActive(false);
    }

    public void OnValueChangePassword(InputField o)
    {

        string passchec01, passchec02, passchec03, passchec04, passchec05, passchec06;

        var input = o.text;
        string ErrorMessage = string.Empty;



        if (string.IsNullOrWhiteSpace(input))
        {
            //throw new Exception("Password should not be empty");
            passchec01 = passchec02 = passchec03 = passchec04 = passchec05 = passchec06 = "✗";
        }
        else
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasMiniMaxChars.IsMatch(input))
            {
                passchec01 = "✗";
            }
            else
            {
                passchec01 = "✓";
            }

            if (!hasNumber.IsMatch(input))
            {
                passchec02 = "✗";
            }
            else
            {
                passchec02 = "✓";
            }

            if (!hasUpperChar.IsMatch(input))
            {
                passchec03 = "✗";
            }
            else
            {
                passchec03 = "✓";
            }

            if (!hasLowerChar.IsMatch(input))
            {
                passchec04 = "✗";
            }
            else
            {
                passchec04 = "✓";
            }

            if (!hasSymbols.IsMatch(input))
            {
                passchec05 = "✗";
            }
            else
            {
                passchec05 = "✓";
            }

            if (__ifPasswordRetype.text != __ifPassword.text)
            {
                passchec06 = "✗";
            }
            else
            {
                passchec06 = "✓";
            }
        }

        

        //Debug.Log(string.Format("Sample:OnValueChange[{1}] ({0})", o.text, o.name)); ✓ ✗❌
        TooltipPassword.SetActive(true);
        String OutputText = "PASSWORD must be\n\n";
        OutputText = OutputText + passchec01+" 8 - 15 characters long\n";
        OutputText = OutputText + passchec02+" Contain a number(0 - 9)\n";
        OutputText = OutputText + passchec03+" Uppercase Letter(A - Z)\n";
        OutputText = OutputText + passchec04+" Lowercase Letter(a - z)\n";
        OutputText = OutputText + passchec05+" Special Character (~!@#$%)\n";
        OutputText = OutputText + passchec06+" Password Must Match\n";

        Text CheckingProcessOutput = TooltipPassword.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        CheckingProcessOutput.text = ""+ OutputText;
    }

    public void OnEndEditChangePassword(InputField o)
    {
        //Debug.Log(string.Format("Sample:OnEndEdit[{1}] ({0})", o.text, o.name));
        TooltipPassword.SetActive(false);
    }


    public void ShowPassword(InputField ifpassword)
    {
        if (ifpassword.contentType == InputField.ContentType.Standard)
        {
            ifpassword.contentType = InputField.ContentType.Standard;
            ifpassword.contentType = InputField.ContentType.Password;
            print("Password now");
        }
        else if (ifpassword.contentType == InputField.ContentType.Password)
        {
            ifpassword.contentType = InputField.ContentType.Password;
            ifpassword.contentType = InputField.ContentType.Standard;
            print("Standard now");
        }
        ifpassword.ForceLabelUpdate();
    }
}
