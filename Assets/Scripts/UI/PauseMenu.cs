using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public static bool isGamePaused;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private GameManager gameManager;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        optionsUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        optionsUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        gameManager.ResumeGame();
    }

    public void Pause()
    {
        gameManager.PauseGame();
        pauseMenuUI.SetActive(true);
        optionsUI.SetActive(false);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void Quit()
    {
        Resume();
        gameManager.QuitToMenu();
    }

    public void ShowOptions()
    {
        pauseMenuUI.SetActive(false);
        optionsUI.SetActive(true);
    }

}
