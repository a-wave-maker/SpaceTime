using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : PlayerWeapon
{

    [SerializeField] private int numberOfBullets = 10;
    [SerializeField] private float spreadAngle = 25f;

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

            for (int i = 0; i < numberOfBullets; i++)
            {
                Quaternion spreadDirection = (Quaternion)direction * Quaternion.Euler(0, 0, Random.Range(-spreadAngle, spreadAngle));

                SpawnBullet(spreadDirection);
            }
            LastFireTime = Time.time;
            RemainingAmmo--;

            if (PlayerPrefs.GetInt("AutoReload").Equals(1))
            {
                if (RemainingAmmo.Equals(0))
                {
                    Reload();
                }
            }

            return true;
        }
        else return false;
    }


}
