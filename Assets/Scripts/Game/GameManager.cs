using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    public enum GameState
    {
        MainMenu,
        Playing,
        GameOver
    }

    public static GameManager Instance; // Singleton pattern

    public GameState currentState = GameState.MainMenu;

    private bool modeSuperHot = false;
    private float superHotMin = 0;
    private float superHotMax = 50;

    public bool ModeSuperHot { get => modeSuperHot; set => modeSuperHot = value; }

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
        PlayerController.ChangeSuperHot += changeMode;
    }

    private void Update()
    {
        if (modeSuperHot)
        {
            setTimeScale(mappedSpeed() + 0.1f);
        }
    }
    private float mappedSpeed()
    {
        float clampedSpeed = Mathf.Clamp(playerData.PlayerRB.velocity.magnitude, superHotMin, superHotMax);

        float mappedValue = Mathf.Lerp(0f, 0.9f, clampedSpeed / superHotMax);

        return mappedValue;
    }

    public void StartGame()
    {
        print("Start"); // TMP
        currentState = GameState.Playing;
        SceneManager.LoadScene("SampleScene");
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

    public void setTimeScale(float amount)
    {
        Time.timeScale = amount;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }

    public void changeMode()
    {
        modeSuperHot = !modeSuperHot;
    }
}
