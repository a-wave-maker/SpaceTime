using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    private float fireRate; // bullets per second
    private float recoil;
    private float reloadTime; // how long it takes to reload
    private int maxAmmo;

    [SerializeField] private Bullet bullet;
    private GameObject owner; // who has the weapon
    private String weaponName;


    public float FireRate
    {
        get { return fireRate; }
        set { fireRate = value; }
    }

    public float Recoil
    {
        get { return recoil; }
        set { recoil = value; }
    }

    public float ReloadTime
    {
        get { return reloadTime; }
        set { reloadTime = value; }
    }

    public int MaxAmmo
    {
        get { return maxAmmo; }
        set { maxAmmo = value; }
    }

    public GameObject Owner
    {
        get { return owner; }
        set { owner = value; }
    }

    public String WeaponName
    {
        get { return weaponName; }
        set { weaponName = value; }
    }

    protected virtual void Start()
    {
        owner = gameObject.transform.parent.gameObject;
    }

    // fire bullet(s)
    public virtual bool Fire(Quaternion? direction = null)
    {
        Bullet newBullet = Instantiate(bullet, transform.position, (Quaternion)direction);

        if (owner != null) {
            newBullet.WeaponVelocity = owner.GetComponent<Rigidbody2D>().velocity;
            newBullet.Owner = owner;
        }
        return true;
    }


    public virtual void Reload(){}

    public virtual void Switch(){}
}
