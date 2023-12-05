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
    private string weaponName;


    public float FireRate { get => fireRate; set => fireRate = value; }
    public float Recoil { get => recoil; set => recoil = value; }
    public float ReloadTime { get => reloadTime; set => reloadTime = value; }
    public int MaxAmmo { get => maxAmmo; set => maxAmmo = value; }
    public Bullet Bullet { get => bullet; set => bullet = value; }
    public GameObject Owner { get => owner; set => owner = value; }
    public string WeaponName { get => weaponName; set => weaponName = value; }


    protected virtual void Start()
    {
        Owner = gameObject.transform.parent.gameObject;
    }

    // fire bullet
    public virtual bool Fire(Quaternion? direction = null)
    {
        Bullet newBullet = Instantiate(Bullet, transform.position, (Quaternion)direction);

        if (Owner != null) {
            newBullet.WeaponVelocity = Owner.GetComponent<Rigidbody2D>().velocity;
            newBullet.Owner = Owner;
        }
        return true;
    }

    public virtual void Reload(){}

    public virtual void Switch(){}
}
