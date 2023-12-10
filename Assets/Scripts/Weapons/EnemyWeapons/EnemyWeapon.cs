using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : Weapon
{

    protected override void Start()
    {

    }

    void Update()
    {

    }

    public override bool Fire(Quaternion? direction = null)
    {
        if (!direction.HasValue)
        {
            direction = transform.rotation;
        }
        base.Fire(direction);
        return true;
    }

}
