using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float fireRate = 5f; // bullets per second
    private float recoil = 5f;

    private float lastFireTime;
    private bool isActive = false;

    [SerializeField] private Bullet bullet;
    private GameObject owner;

    public float FireRate
    {
        get { return fireRate; }
        set { fireRate = value; }
    }

    public float Recoil
    {
        get { return recoil; }
        set { recoil = value; }
    }

    public GameObject Owner
    {
        get { return owner; }
        set { owner = value; }
    }


    void Start()
    {
        // prepare lastFireTime to allow for instant firing
        lastFireTime = - (1 / fireRate);
    }

    public bool Fire(Quaternion? direction = null)
    {
        if (!direction.HasValue)
        {
            direction = transform.rotation;
        }

        if (CanFire())
        {
            Bullet newBullet = Instantiate(bullet, transform.position, (Quaternion)direction);

            if (owner != null) {
                newBullet.WeaponVelocity = owner.GetComponent<Rigidbody2D>().velocity; // this is probably temporary
                newBullet.Owner = owner;
            }

            lastFireTime = Time.time;
            return true;
        }
        else return false;
    }

    public bool CanFire()
    {
        return Time.time - lastFireTime >= 1 / fireRate;
    }


    public void Switch() // ??
    {
        // reset weapon
        if (isActive)
        {
            isActive = false;
            lastFireTime = - (1 / fireRate);
            print("weapon off");
        }
        else
        {
            isActive = true;
            print("weapon on");
        }
    }
}
