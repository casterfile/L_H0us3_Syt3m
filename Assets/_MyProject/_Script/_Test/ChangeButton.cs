using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("y"))
        {
            print("space key was pressed");
            SceneManager.LoadScene("CSM_Scene01");
        }
        if (Input.GetKeyDown("x"))
        {
            print("space key was pressed");
            SceneManager.LoadScene("HotspotClient");
        }
    }

}
