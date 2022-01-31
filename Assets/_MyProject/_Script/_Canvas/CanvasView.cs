using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasView : MonoBehaviour
{
    [SerializeField] private bool isEnlargeView = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScreenSizeOptimize();
    }

    private void ScreenSizeOptimize()
    {
        if (Global_Variable.isKeyboardOn == false)
        {

        }

        float Match = (Screen.width - Screen.height);
        CanvasScaler scaler = GetComponent<CanvasScaler>();
        if (Match < 0)
        {
            if(isEnlargeView == true)
            {
                scaler.matchWidthOrHeight = 0.7f;
            }
            else
            {
                float FinalMatch = -(Match / Screen.height);
                //print("Match: " + FinalMatch);

                //scaler.scaleFactor = 2.7f;
                //scaler.scaleFactor = 2.7f;
                scaler.matchWidthOrHeight = FinalMatch;
            }
            
        }
        else
        {
            scaler.matchWidthOrHeight = 0;
        }
    }
}
