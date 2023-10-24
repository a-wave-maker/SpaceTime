using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
<<<<<<< HEAD
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
=======
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
>>>>>>> 4acb0d6 (added demo weapon and bullet scripts)
    }
}
