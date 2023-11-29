using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 10f;
    private Vector3 weaponVelocity = Vector3.zero;
    private int damage = 5;

    private Animator anim;
    private Rigidbody2D rb;
    private GameObject owner;
    private TrailRenderer trail;

    private float lifeTime = 5f;
    // public float trailtime = 0.5f;
    private float trailWidthDecreaseRate = 0.3f;
    private float trailLengthDecreaseRate = 0.0005f;


    public Vector3 WeaponVelocity { get => weaponVelocity; set => weaponVelocity = value; }
    public GameObject Owner { get => owner; set => owner = value; }
    public int Damage { get => damage; set => damage = value; }


    void Start()
    {
        anim = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * speed + WeaponVelocity, ForceMode2D.Impulse);

        trail = GetComponentInChildren<TrailRenderer>();

        Invoke("Despawn", lifeTime);
    }

    void Update()
    {
        // decrease the trail following bullet
        if (trail != null)
        {
            trail.widthMultiplier -= trailWidthDecreaseRate * Time.deltaTime;
            trail.time -= trailLengthDecreaseRate;

            if (trail.widthMultiplier < 0.05f)
            {
                trail.widthMultiplier = 0.05f;
            }
            if (trail.time < 0f)
            {
                trail.time = 0f;
            }
        }
    }

    void Despawn()
    {
        anim.SetTrigger("Despawn");
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

}
