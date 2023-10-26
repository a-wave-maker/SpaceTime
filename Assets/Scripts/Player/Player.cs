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

    public void switchWeapons(char type, int number = 1)
    {
        // type == +, number == 1 -> switches to the next weapon
        // type == -, number == 2 -> switches to the prev prev weapon
        // type == =, number == 5 -> switches to the 5th weapon

        int boundary = playerData.PlayerWeapons.Count;
        
        if (boundary <= 0) { 
            return;             // No weapons to switch to -> return
        }

        // playerData.PlayerActiveWeapon.Switch(); // TODO
        int nextIndex = playerData.PlayerActiveWeaponIdx;

        switch (type)
        {
            case '+':
                nextIndex = (playerData.PlayerActiveWeaponIdx + number) % playerData.PlayerWeapons.Count;
                break;
            case '-':
                nextIndex = (playerData.PlayerActiveWeaponIdx - number) % playerData.PlayerWeapons.Count;
                break;
            case '=':
                nextIndex = number % playerData.PlayerWeapons.Count;
                break;
            default:
                break;
        }

        playerData.PlayerActiveWeaponIdx = nextIndex;
    }

    public void Fire()
    {
        if (playerData.PlayerActiveWeaponIdx == 0)
        {
            return;
        }
        // Fire weapon
        Weapon currentWeapon = playerData.PlayerActiveWeapon;
        currentWeapon.Fire();

        // Apply recoil
        /*float force = currentWeapon.Recoil;*/ // TODO
        float force = 5f; // TMP

        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0f;

        Vector2 direction = transform.position - targetPosition;

        applyForce(force, direction);
    }

    public void Reload()
    {
        print("Reloading"); // TMP
        // TODO
    }
}
