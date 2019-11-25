using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private static bool GamePaused = false;

    [SerializeField]
    private GameObject PauseMenuUI;

    [SerializeField]
    private Button resumeButton;

    [SerializeField]
    private Button restartButton;

    [SerializeField]
    private Button menuButton;

    public Button[] mainMenu;

    private bool canInteract = true;
    private int curChoice = 0;

    private void Update()
    {
        int verticalInput = (int)Input.GetAxis("DpadVertical");

        if (Input.GetButtonDown("Start"))
        {
            onClickPause();
        }

        if (GamePaused == true)
        {
            mainMenu[curChoice].GetComponentInChildren<Text>().color = UnityEngine.Color.red;
            if (verticalInput < 0 && canInteract)
            {
                canInteract = false;
            }
        }
    }

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
