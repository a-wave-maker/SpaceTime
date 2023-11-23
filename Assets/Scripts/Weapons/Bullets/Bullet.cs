using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 10f;
    private Vector3 weaponVelocity = Vector3.zero;
    private float damage = 1f;

    private Rigidbody2D rb;
    private GameObject owner;
    // private TrailRenderer trail;

    private float lifeTime = 5f;
    // public float trailtime = 0.5f;

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
    public GameObject Owner
    {
        get { return owner; }
        set { owner = value; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * speed + weaponVelocity, ForceMode2D.Impulse);

        // trail = GetComponentInChildren<TrailRenderer>();

        Invoke("DestroyBullet", lifeTime);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

}
