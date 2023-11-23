using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : Weapon
{


    protected virtual void Start()
    {

    }

    void Update()
    {

    }

    public override bool Fire(Quaternion? direction = null)
    {
        // if (!direction.HasValue)
        // {
        //     direction = transform.rotation;
        // }
        // // print(direction);
        // if (CanFire())
        // {
        //     base.Fire(direction);
        //     lastFireTime = Time.time;
        //     remainingAmmo--;
        //     return true;
        // }
        // else return false;
        return false;
    }



}
