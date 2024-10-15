using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KillAll : BaseGamemode
{
    private int enemyCount = 1;
    private bool playerDead = false;
    private bool won = false;

    private GameData gameData;


    // Start is called before the first frame update
    void Start()
    {
        // GameData.OnDataLoaded += OnDataLoaded;

        gameData = GameObject.Find("GameManager").GetComponent<GameData>();

        // Subscribe to the EnemyDeath and PlayerDeath events
        DamageableEnemy.EnemyDeath += HandleEnemyDeath;
        Player.PlayerDeath += HandlePlayerDeath;
    }

    private void Update()
    {
        if (gameData != null) {
            enemyCount = gameData.EnemyCount;
        } 
        else
        {
            gameData = GameObject.Find("GameManager").GetComponent<GameData>();
        }
    }

    /*private void OnDataLoaded(GameData data)
    {
        enemyCount = gameData.EnemyCount;
    }*/

    private void HandleEnemyDeath(GameObject enemy)
    {
        enemyCount--;
        gameData.Score += enemy.GetComponent<DamageableEnemy>().Value;

        if (enemyCount <= 0)
        {
            won = true;
        }
    }

    private void HandlePlayerDeath()
    {
        playerDead = true;
    }

    public override bool WinConditionMet()
    {
        // print(gameData.Score);
        gameData.Score = (int)(gameData.Score / (gameData.TimePassed / 100));
        // print(gameData.Score);
        // print(gameData.TimePassed);
        return won;
    }

    public override bool LossConditionMet()
    {
        if(playerDead)
        {
            playerDead = false;
            return true;
        }

        return false;
    }

    public override void ResetMode()
    {
        won = false;
        enemyCount = 1;
    }
}
