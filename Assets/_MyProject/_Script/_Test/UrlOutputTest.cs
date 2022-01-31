using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
 
public class UrlOutputTest : MonoBehaviour
{
    public TMP_InputField __OutputTextFromWeb;
    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://192.168.1.6/LuxHouseSystem/Server/UpdateData.php");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            //Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            //byte[] results = www.downloadHandler.data;

            string[] _urlTextQuery = www.downloadHandler.text.Split("|"[0]);
            string _strOutputAppend = "";
            foreach (string _textQuery in _urlTextQuery)
            {
                //print(_textQuery);
                _strOutputAppend += _textQuery + "\n";

                string[] _newLine = _textQuery.Split("|"[0]);
                foreach (string _checkNewlind in _newLine)
                {
                    _strOutputAppend += "\n\n\n\n";
                }

             }
            __OutputTextFromWeb.text = ""+ _strOutputAppend;
        }
    }
}
