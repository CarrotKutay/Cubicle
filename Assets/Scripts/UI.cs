using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public void onClickPlay()
    {
        SceneManager.LoadScene("GameSettings");

    }
    public void onClickSettings()
    {
        SceneManager.LoadScene("GeneralSettings");
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

