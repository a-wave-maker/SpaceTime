using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAll : BaseGamemode
{
    private int enemyCount = 1;
    private bool playerDead = false;
    private bool won = false;
    private const int ENEMY_VALUE = 10;

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
        gameData.Score += ENEMY_VALUE;

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
