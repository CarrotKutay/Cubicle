using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIStage : MonoBehaviour
{
    private static bool GamePaused = false;

    [SerializeField]
    private GameObject PauseMenuUI;

    [SerializeField]
    private Character player1;
    private int healthP1;
    [SerializeField]
    private Text healthTextP1;

    [SerializeField]
    private Character player2;
    private int healthP2;
    [SerializeField]
    private Text healthTextP2;

    [SerializeField]
    private Text winnerText;
    [SerializeField]
    private GameObject EndMenuUI;

    private void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            onClickPause();
        }

        healthP1 = player1.health;
        healthP2 = player2.health;

        if (healthP1 < 0)
        {
            healthP1 = 0;
        }
        if (healthP2 < 0)
        {
            healthP2 = 0;
        }

        healthTextP1.text = "Health: " + healthP1;
        healthTextP2.text = "Health: " + healthP2;

        if (player1.transform.position.y < -15f || player1.health <= 0)
        {
            EndMenuUI.SetActive(true);
            winnerText.text = "Player Red Wins!";
        }
        else if (player2.transform.position.y < -15f || player2.health <= 0)
        {
            EndMenuUI.SetActive(true);
            winnerText.text = "Player Green Wins!";
        }
    }

    public void onClickPause()
    {
        if (GamePaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GamePaused = false;
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GamePaused = true;
    }

    public void Menu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
