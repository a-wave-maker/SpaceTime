using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        MainMenu,
        Playing,
        GameOver
    }

    public static GameManager Instance; // Singleton pattern

    public GameState currentState = GameState.MainMenu;

    void Start()
    {
        // Make sure there is only one GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep the GameManager object between scenes
        }
        else
        {
            Destroy(gameObject);
        }

        // Subscribe to the PlayerDeath event
        Player.PlayerDeath += HandlePlayerDeath;

    }

    public void StartGame()
    {
        currentState = GameState.Playing;
        Cursor.visible = false;
        SceneManager.LoadScene("Level1");
    }

    public void GameOver()
    {
        Cursor.visible = true;
        currentState = GameState.GameOver;
        SceneManager.LoadScene("MainMenu");
    }

    public void HandlePlayerDeath()
    {
        GameOver();
    }
}
