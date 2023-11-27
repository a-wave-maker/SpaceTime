using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : PlayerWeapon
{
    protected override void Start()
    {
        FireRate = 8f;
        Recoil = 16f;
        ReloadTime = 1f;
        MaxAmmo = 10;
        WeaponName = "Shotgun";
        // test values ^
        base.Start();
    }

    // void Update()
    // {

    // }

    public override bool Fire(Quaternion? direction = null)
    {
        if (!direction.HasValue)
        {
            direction = transform.rotation;
        }
        if (CanFire())
        {
            Bullet newBullet = Instantiate(Bullet, transform.position, (Quaternion)direction);

            if (Owner != null) {
                newBullet.WeaponVelocity = Owner.GetComponent<Rigidbody2D>().velocity;
                newBullet.Owner = Owner;
            }
            LastFireTime = Time.time;
            RemainingAmmo--;
            return true;
        }
        else return false;
    }


}
