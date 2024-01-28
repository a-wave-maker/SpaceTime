using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    List<GameObject> enemies;
    int enemyCount;
    float timePassed = 0f;

    public delegate void DataLoaded(GameData data);
    public static event DataLoaded OnDataLoaded;

    public int EnemyCount { get => enemyCount; set => enemyCount = value; }

    // Start is called before the first frame update
    void Start()
    {
        // Subscribe to the EnemyDeath event
        // DamageableEnemy.EnemyDeath += HandleEnemyDeath;

        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        enemyCount = enemies.Count;
    }

    public void LoadData()
    {
        // get all enemies
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        EnemyCount = enemies.Count;

        OnDataLoaded.Invoke(this);
    }

    /*private void HandleEnemyDeath(GameObject enemy)
    {
        enemies.Remove(enemy);
        EnemyCount--;
    }*/
}
