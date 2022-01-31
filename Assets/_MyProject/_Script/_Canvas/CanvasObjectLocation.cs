using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasObjectLocation : MonoBehaviour
{
    public enum Orientation { TopLeft, TopRight, BottmLeft, BottomRight }
    public Orientation orientation;

    RectTransform ObjectRect;

    private void Awake()
    {
        ObjectRect = gameObject.GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (orientation == Orientation.TopLeft)
        {

        }
        else if (orientation == Orientation.TopRight)
        {

        }
        else if (orientation == Orientation.BottmLeft)
        {

        }
        else if (orientation == Orientation.BottomRight)
        {
            BottomRight();
        }
        
    }

    private void BottomRight()
    {
        float Match = (Screen.width - Screen.height);
        CanvasScaler scaler = GetComponent<CanvasScaler>();
        if (Match < 0)
        {
            
            ObjectRect.position = new Vector2(Screen.width - (ObjectRect.rect.width * 2), Screen.height - (Screen.height - (Screen.height * 0.15f)));

        }
        else
        {
            ObjectRect.position = new Vector2(Screen.width - (ObjectRect.rect.width * 2), Screen.height - (Screen.height - (Screen.height * 0.35f)));
        }
    }

}
