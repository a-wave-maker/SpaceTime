using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            print("AAAAAAAAAAAAAAAA");
        }
    }

    private void applyForce(float force, Vector2 direction)
    {
        Rigidbody2D playerRB = playerData.PlayerRB;

        playerRB.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }

    public void Fire()
    {
        // Fire weapon
        Weapon currentWeapon = playerData.PlayerActiveWeapon;
        currentWeapon.Fire();

        // Apply recoil
        /*float force = currentWeapon.Recoil;*/ // TODO
        float force = 5f;

        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0f;

        Vector2 direction = transform.position - targetPosition;

        applyForce(force, direction);
    }

    public void Reload()
    {
        print("Reloading");
        // TODO
    }
}
