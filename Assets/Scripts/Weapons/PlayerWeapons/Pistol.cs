using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : PlayerWeapon
{
    protected override void Start()
    {
        FireRate = 0.5f;
        Recoil = 5f;
        ReloadTime = 2f;
        MaxAmmo = 6;
        WeaponName = "Pistol";
        base.Start();
    }

/*    public override bool Fire(Quaternion? direction = null)
    {
        if (base.Fire(direction))
        {
            return true;
        } else return false;
    }*/


}