using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyWeapon : Weapon
{

    public override bool Fire(Quaternion? direction = null)
    {
        return false;
    }


}
