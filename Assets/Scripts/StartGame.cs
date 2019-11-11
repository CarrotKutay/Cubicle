using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Toggle level1;
    public Toggle level2;
    public Toggle level3;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void chooseStage()
    { 
        if (level1.isOn)
        {
            SceneManager.LoadScene("MistyMountains");
        }
        else if (level2.isOn)
        {
            SceneManager.LoadScene("TommyStage");
        }
        // Level 3...
    }
}
