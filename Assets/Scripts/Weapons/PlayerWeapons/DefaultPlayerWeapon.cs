using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPlayerWeapon : PlayerWeapon
{
    protected override void Start()
    {
        Recoil = 0;
        MaxAmmo = 0;
        WeaponName = "Default";
        // test values ^
        base.Start();
    }

    // protected override void Start()
    // {
    //     // FireRate = 0f;
    //     // Recoil = 0f;
    // }

    public override bool Fire(Quaternion? direction = null)
    {
        return false;
    }


}
