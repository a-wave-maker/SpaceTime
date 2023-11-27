using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    private GameObject owner; // who has the weapon

    public GameObject Owner
    {
        get { return owner; }
        set { owner = value; }
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


    public virtual void Reload()
    {

    }

    public virtual void Switch()
    {

    }
}
