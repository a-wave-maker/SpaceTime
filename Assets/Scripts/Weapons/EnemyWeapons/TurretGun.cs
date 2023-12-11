using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGun : EnemyWeapon
{


    protected override void Start()
    {
        FireRate = 1f;
        base.Start();
    }

    void Update()
    {

    }


    public override bool Fire(Quaternion? direction = null)
    {
        if (base.Fire(direction))
        {
            return true;
        } else return false;
    }

}
