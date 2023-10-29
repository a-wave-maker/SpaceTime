using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    private Vector3 weaponVelocity = Vector3.zero;
    private float damage;
    private Rigidbody2D rb;

    private float lifeTime = 5f;

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public Vector3 WeaponVelocity
    {
        get { return weaponVelocity; }
        set { weaponVelocity = value; }
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    void Start()
    {
        speed = 5f;
        damage = 1f;

        rb = GetComponent<Rigidbody2D>();
        // print("weapon velocity:" + weaponVelocity);
        rb.AddForce((transform.up + weaponVelocity) * speed, ForceMode2D.Impulse); // change bullet speed relative to weapon speed (TODO)
        // print(rb.velocity);
    }

    void Update()
    {
        // despawn bullet after x time
        Destroy(gameObject, lifeTime);
    }
}
