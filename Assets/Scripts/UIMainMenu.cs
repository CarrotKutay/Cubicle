using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuUI;
    [SerializeField]
    private GameObject gameSettingsUI;
    [SerializeField]
    private GameObject controlUI;

    [SerializeField]
    private Toggle level1;
    [SerializeField]
    private Toggle level2;
    [SerializeField]
    private Toggle level3;


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

    public void onClickStartGame()
    {
        if (level1.isOn)
        {
            SceneManager.LoadScene("MistyMountains");
        }
        else if (level2.isOn)
        {
            SceneManager.LoadScene("TommyStage");
        }
        else if (level3.isOn)
        {
            SceneManager.LoadScene("SunnyBeach");
        }
    }
}

