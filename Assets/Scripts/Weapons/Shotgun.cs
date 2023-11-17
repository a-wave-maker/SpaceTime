using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{

    void Start()
    {
        FireRate = 3f;
        Recoil = 8f;
    }

    void Update()
    {

    }

    public override bool Fire(Quaternion? direction = null)
    {
        // if (direction.HasValue)
        // {
        //     base.Fire(direction);
        // } else
        // {
        //     base.Fire();
        // }

        print("firing a shotgun");
        return true;
    }


}
