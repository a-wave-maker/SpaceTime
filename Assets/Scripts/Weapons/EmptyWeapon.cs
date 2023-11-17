using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyWeapon : Weapon
{

    void Start()
    {
        FireRate = 0f;
        Recoil = 0f;
    }

    public override bool Fire(Quaternion? direction = null)
    {
        return false;
    }


}
