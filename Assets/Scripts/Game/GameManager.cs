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

        // temp? set default settings
        PlayerPrefs.SetInt("BulletCollision", 1); // 1=on, 0=off
        PlayerPrefs.Save();
    }

    public void StartGame()
    {
        print("Start"); // TMP
        currentState = GameState.Playing;
        SceneManager.LoadScene("DemoTest");
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;
        SceneManager.LoadScene("MainMenu");
    }

    public void HandlePlayerDeath()
    {
        currentState = GameState.GameOver;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitToMenu()
    {
        currentState = GameState.MainMenu;
        SceneManager.LoadScene("MainMenu");
    }

}
