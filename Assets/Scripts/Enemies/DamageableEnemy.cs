using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class DamageableEnemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int value = 10;
    public int Value { get => value; set => this.value = value; }

    public float startingHealth;
    protected float health;
    protected bool dead;

    public delegate void EnemyDeathAction(GameObject enemy);
    public static event EnemyDeathAction EnemyDeath;
    protected virtual void Start()
    {
        health = startingHealth;
    }
    
    public void TakeHit(float damage)
    {
        health -= damage;

        if (health <= 0 && !dead)
        {
            dead = true;
            Die();
        }
    }

    protected void Die()
    {
        EnemyDeath?.Invoke(gameObject);
        GameObject.Destroy(gameObject);
    }
}