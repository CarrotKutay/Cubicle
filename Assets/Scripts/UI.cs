using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickPlay()
    {
        SceneManager.LoadScene("MistyMountains");

    }
    public void onClickSettings()
    {
        SceneManager.LoadScene("MistyMountains");
    }

    public void onClickControl()
    {
        SceneManager.LoadScene("ControlScene");
    }

    public void onClickQuit()
    {
        Application.Quit();
    }

    public void onClickBackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

