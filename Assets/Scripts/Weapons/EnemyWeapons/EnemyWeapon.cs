using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : Weapon
{

    private float lastFireTime;

    public float LastFireTime { get => lastFireTime; set => lastFireTime = value; }

    protected override void Start()
    {
        base.Start();
        LastFireTime = 0;
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
        if (CanFire())
        {
            base.Fire(direction);
            LastFireTime = Time.time;
            return true;
        } else return false;
    }

    public bool CanFire()
    {
        return Time.time - LastFireTime >= 1f; // temporary: FireRate property needs fixing
    }

}
