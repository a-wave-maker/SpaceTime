using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float fireRate = 5f; // bullets per second
    private float recoil = 5f;

    private float lastFireTime;
    [SerializeField] private Bullet bullet;

    private bool isActive = false;

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

    void Start()
    {
        // prepare lastFireTime to allow for instant firing
        lastFireTime = - (1 / fireRate);
    }

    public bool Fire()
    {
        if (CanFire())
        {
            Instantiate(bullet, transform.position, transform.rotation);
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
