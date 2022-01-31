using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckScreenSize : MonoBehaviour
{
    [SerializeField]  private Text Output;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Output.text = "Checking Screen Size Screen.width: "+ Screen.width + "|| Screen.height: " + Screen.height; 
    }
}
