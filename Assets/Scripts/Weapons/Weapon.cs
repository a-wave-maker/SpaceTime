using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float fireRate;
    private float recoil;
    [SerializeField] private Bullet bullet;

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

    void Start()
    {
        // set firerate and recoil?
    }

    public void Fire()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
