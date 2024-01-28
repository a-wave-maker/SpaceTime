using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    List<GameObject> enemies;
    int enemyCount;
    float timePassed = 0f;

    public int EnemyCount { get => enemyCount; set => enemyCount = value; }

    // Start is called before the first frame update
    void Start()
    {
        // get all enemies
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        EnemyCount = enemies.Count;

        // Subscribe to the EnemyDeath event
        DamageableEnemy.EnemyDeath += HandleEnemyDeath;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
    }

    private void HandleEnemyDeath(GameObject enemy)
    {
        enemies.Remove(enemy);
        EnemyCount--;
    }
}
