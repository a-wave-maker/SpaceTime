using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class DamageableEntity : MonoBehaviour, IDamageable
{
    public float startingHealth;
    protected float health;
    protected bool dead;

    public void Start()
    {
        health = startingHealth;
    }

    public void TakeHit(float damage, RaycastHit hit)
    {
        health -= damage;

        if(health < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        dead = true;
    }
}