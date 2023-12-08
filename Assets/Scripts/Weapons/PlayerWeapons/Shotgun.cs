using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : PlayerWeapon
{
    protected override void Start()
    {
        FireRate = 0.3f;
        Recoil = 15f;
        ReloadTime = 0.3f;
        MaxAmmo = 1;
        WeaponName = "Shotgun";
        // test values ^
        base.Start();
    }

    private void SpawnBullet(Quaternion direction)
    {
        Bullet newBullet = Instantiate(Bullet, transform.position, direction);

        if (Owner != null)
        {
            newBullet.WeaponVelocity = Owner.GetComponent<Rigidbody2D>().velocity;
            newBullet.Owner = Owner;
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
            float spreadAngle = 25f;

            for (int i = 0; i < 7; i++)
            {
                Quaternion spreadDirection = (Quaternion)direction * Quaternion.Euler(0, 0, Random.Range(-spreadAngle, spreadAngle));

                SpawnBullet(spreadDirection);
            }
            LastFireTime = Time.time;
            RemainingAmmo--;
            return true;
        }
        else return false;
    }


}
