using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class DamageableEntity : MonoBehaviour, IDamageable
{
    public float startingHealth;
    protected float health;
    protected bool dead;

    protected virtual void Start()
    {
        health = startingHealth;
    }

    protected void TakeHit(float damage, RaycastHit hit)
    {
        health -= damage;

        if(health < 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        dead = true;
        GameObject.Destroy(gameObject);
    }
}