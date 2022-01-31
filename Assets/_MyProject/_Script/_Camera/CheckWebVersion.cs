using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CheckWebVersion : MonoBehaviour
{
    public Text txtOutput;
    // Start is called before the first frame update
    void Start()
    {
        print(SystemInfo.graphicsDeviceType);
        txtOutput.text = "Output: "+ SystemInfo.graphicsDeviceType;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
