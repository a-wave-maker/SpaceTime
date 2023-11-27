using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : Weapon
{

    private float fireRate; // bullets per second
    private float recoil;
    private float reloadTime; // how long it takes to reload
    private int maxAmmo;

    private int remainingAmmo;
    private float lastFireTime;
    private float reloadCooldown;

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

    public float ReloadTime
    {
        get { return reloadTime; }
        set { reloadTime = value; }
    }

    public int MaxAmmo
    {
        get { return maxAmmo; }
        set { maxAmmo = value; }
    }


    protected override void Start()
    {
        base.Start();
        lastFireTime = - (1 / fireRate);
        remainingAmmo = maxAmmo;
        reloadCooldown = Time.time;
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


    public override bool Fire(Quaternion? direction = null)
    {
        if (!direction.HasValue)
        {
            direction = transform.rotation;
        }
        if (CanFire())
        {
            base.Fire(direction);
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
        print(hasAmmo);
        float shotCooldown = 1 / fireRate;
        return hasAmmo && Time.time - lastFireTime >= shotCooldown;
    }

     // reload
    public override void Reload()
    {
        remainingAmmo = 0;
        reloadCooldown = Time.time;
        isReloading = true;
    }

    // switch weapon active status
    public override void Switch()
    {
        if (isActive)
        {
            isActive = false;
            gameObject.SetActive(false); // unrenders object
        }
        else
        {
            isActive = true;
            gameObject.SetActive(true); // renders object
        }
    }

}
