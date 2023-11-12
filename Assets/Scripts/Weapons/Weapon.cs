using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float fireRate = 5f; // bullets per second
    private float recoil = 2f;
    private float reloadTime = 2f; // how long it takes to reload
    private int maxAmmo = 6;


    private int remainingAmmo;
    private float lastFireTime;
    private float reloadCooldown;


    [SerializeField] private Bullet bullet;
    private GameObject owner; // who has the weapon

    private bool isActive = false;
    private bool isReloading = false;

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
        lastFireTime = - (1 / fireRate);
        remainingAmmo = maxAmmo;
    }

    void Update()
    {
        // if reloading -> check if finished
        if (isReloading && Time.time - reloadCooldown >= reloadTime)
        {
            remainingAmmo = maxAmmo;
            isReloading = false;
        }
    }

    // fire bullet(s)
    public virtual bool Fire(Quaternion? direction = null)
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
            remainingAmmo--;
            return true;
        }
        else return false;
    }

    // check if possible to fire a bullet
    private bool CanFire()
    {
        bool hasAmmo = remainingAmmo > 0;
        float shotCooldown = 1 / fireRate;
        return hasAmmo && Time.time - lastFireTime >= shotCooldown;
    }

    // reload
    public void Reload()
    {
        reloadCooldown = Time.time;
        isReloading = true;
    }

    // switch weapon active status
    public void Switch()
    {
        print("switching weapon");
        if (isActive)
        {
            isActive = false;
            gameObject.SetActive(false); // unrenders object
            // lastFireTime = - (1 / fireRate);
        }
        else
        {
            isActive = true;
            gameObject.SetActive(true); // renders object
        }
    }
}
