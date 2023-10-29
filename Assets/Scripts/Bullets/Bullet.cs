using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private float damage;
    private Rigidbody2D rb;

    private float lifeTime = 5f;

    void Start()
    {
        speed = 5f;
        damage = 1f;

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);
        // print(rb.velocity);
    }

    void Update()
    {
        // despawn bullet after x time
        Destroy(gameObject, lifeTime);
    }
}
