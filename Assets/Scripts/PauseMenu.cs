using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static bool GamePaused = false;

    [SerializeField]
    private GameObject PauseMenuUI; 

    public void onClickPause()
    {
        if (GamePaused)
        {
            resume();
        }
        else
        {
            pause();
        }
    }

    public void resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GamePaused = false;
    }

    void pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GamePaused = true;
    }

    public void menu()
    {
        resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void restart()
    {
        resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
