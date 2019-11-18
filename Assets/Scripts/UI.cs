using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuUI;

    [SerializeField]
    private GameObject gameSettingsUI;

    [SerializeField]
    private GameObject controlUI;


    public void onClickPlay()
    {
        mainMenuUI.SetActive(false);
        gameSettingsUI.SetActive(true);
    }

    public void onClickControl()
    {
        mainMenuUI.SetActive(false);
        controlUI.SetActive(true);
    }

    public void onClickQuit()
    {
        Application.Quit();
    }

    public void onClickBackToMenu()
    {
        mainMenuUI.SetActive(true);
        controlUI.SetActive(false);
        gameSettingsUI.SetActive(false);
    }
}

