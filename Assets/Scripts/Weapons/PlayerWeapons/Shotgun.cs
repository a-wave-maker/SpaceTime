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
        if (base.Fire(direction))
        {
            return true;
        } else return false;
    }


}
