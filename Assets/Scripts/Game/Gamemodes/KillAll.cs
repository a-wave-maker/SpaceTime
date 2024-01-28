using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAll : BaseGamemode
{
    private int enemyCount;
    private bool playerDead = false;

    // Start is called before the first frame update
    void Start()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(gameManager.gameData == null)
        {
            print("What");
        }
        enemyCount = gameManager.gameData.EnemyCount;

        // Subscribe to the EnemyDeath and PlayerDeath events
        DamageableEnemy.EnemyDeath += HandleEnemyDeath;
        Player.PlayerDeath += HandlePlayerDeath;
    }

    private void Update()
    {
        
    }

    private void HandleEnemyDeath(GameObject enemy)
    {
        enemyCount--;
    }

    private void HandlePlayerDeath()
    {
        playerDead = true;
    }

    public override bool WinConditionMet()
    {
        if(enemyCount == 0)
        {
            return true;
        }

        return false;
    }

    public override bool LossConditionMet()
    {
        if(playerDead)
        {
            return true;
        }

        return false;
    }
}
