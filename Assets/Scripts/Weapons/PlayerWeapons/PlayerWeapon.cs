using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : Weapon
{

    private int remainingAmmo;
    private float lastFireTime;
    private float reloadStart;

    private bool isActive = false;
    private bool isReloading = false;
    private float reloadProgress = 0f;
    private float switchTime;

    [SerializeField] private bool dynamicReloading; // change reload type (temporary)


    public int RemainingAmmo { get => remainingAmmo; set => remainingAmmo = value; }
    public float LastFireTime { get => lastFireTime; set => lastFireTime = value; }
    public float ReloadStart { get => reloadStart; set => reloadStart = value; }
    public bool IsReloading { get => isReloading; set => isReloading = value; }
    public float ReloadProgress { get => reloadProgress; set => reloadProgress = value; }

    protected override void Start()
    {
        base.Start();
        LastFireTime = - (1 / FireRate);
        RemainingAmmo = MaxAmmo;
        ReloadStart = Time.time;
    }

    void Update()
    {
        // check if reloading
        if (IsReloading && isActive)
        {
            ReloadProgress = Mathf.Clamp((Time.time - ReloadStart) / ReloadTime, 0, 1);
            // print(reloadProgress);

            // if finished reloading
            if (ReloadProgress >= 1) {
                RemainingAmmo = MaxAmmo;
                lastFireTime = 0;
                IsReloading = false;
            }
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
            LastFireTime = Time.time;
            RemainingAmmo--;
            return true;
        }
        else return false;
    }

    // check if possible to fire a bullet
    public bool CanFire()
    {
        bool hasAmmo = RemainingAmmo > 0;
        float shotCooldown = 1 / FireRate;
        return hasAmmo && Time.time - LastFireTime >= shotCooldown;
    }

     // reload
    public override void Reload()
    {
        RemainingAmmo = 0;
        ReloadStart = Time.time;
        IsReloading = true;
        ReloadProgress = 0f;
    }

    // switch weapon active status
    public override void Switch()
    {
        if (isActive)
        {
            isActive = false;
            gameObject.SetActive(false); // unrenders object
            switchTime = Time.time;
        }
        else
        {
            isActive = true;
            gameObject.SetActive(true); // renders object
            if (isReloading) {
                if (dynamicReloading) {
                    // dynamic reloading: keep reloadProgress, adjust reloadStart time to resume reloading from the point it was stopped at
                    float timePassed = Time.time - switchTime;
                    ReloadStart += timePassed;
                } else {
                    // normal reloading: reset realoading completely when switching back
                    isReloading = false;
                    reloadProgress = 0f;
                }
            }
        }
    }

}
