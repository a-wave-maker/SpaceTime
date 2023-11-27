using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : Weapon
{

    private int remainingAmmo;
    private float lastFireTime;
    private float reloadCooldown;

    private bool isActive = false;
    private bool isReloading = false;


    protected override void Start()
    {
        base.Start();
        lastFireTime = - (1 / FireRate);
        remainingAmmo = MaxAmmo;
        reloadCooldown = Time.time;
    }

    void Update()
    {
        // if reloading -> check if finished
        if (isReloading && Time.time - reloadCooldown >= ReloadTime)
        {
            remainingAmmo = MaxAmmo;
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
        float shotCooldown = 1 / FireRate;
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
