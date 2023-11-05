using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    public delegate void PlayerDeathAction();
    public static event PlayerDeathAction PlayerDeath;


    // ----------------------------------------------------------------------------------------------------------------
    // PLAYER ACTIONS
    // ----------------------------------------------------------------------------------------------------------------

    public void Fire()
    {
        if (playerData.PlayerActiveWeaponIdx == 0)
        {
            return;
        }
        // Fire weapon
        Weapon currentWeapon = playerData.PlayerActiveWeapon;

        if (currentWeapon.Fire())
        {
            // Apply recoil
            /*float force = currentWeapon.Recoil;*/ // TODO
            float force = 5f; // TMP

            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0f;

            Vector2 direction = transform.position - targetPosition;

            applyForce(force, direction);
        }
    }

    public void Reload()
    {
        print("Reloading"); // TMP
        // playerData.PlayerActiveWeapon.Reload(); // TODO
    }

    
    // ----------------------------------------------------------------------------------------------------------------
    // WEAPON SWITCHING
    // ----------------------------------------------------------------------------------------------------------------

    public void nextWeapon()
    {
        int nextIdx = playerData.PlayerActiveWeaponIdx;
        if (nextIdx == playerData.PlayerWeapons.Count - 1)
        {
            nextIdx = 0;
        }
        else
        {
            nextIdx = playerData.PlayerActiveWeaponIdx + 1;
        }

        // playerData.PlayerWeapons[PlayerActiveWeaponIdx].Switch(); // TODO
        playerData.PlayerActiveWeaponIdx = nextIdx;
        // playerData.PlayerWeapons[PlayerActiveWeaponIdx].Switch(); // TODO
    }

    public Weapon previousWeapon()
    {
        int nextIdx = playerData.PlayerActiveWeaponIdx;

        if (nextIdx == 0)
        {
            nextIdx = playerData.PlayerWeapons.Count - 1;
        }
        else
        {
            nextIdx = playerData.PlayerActiveWeaponIdx - 1;
        }

        // playerData.PlayerWeapons[PlayerActiveWeaponIdx].Switch(); // TODO
        playerData.PlayerActiveWeaponIdx = nextIdx;
        // playerData.PlayerWeapons[PlayerActiveWeaponIdx].Switch(); // TODO

        return playerData.PlayerWeapons[nextIdx];
    }

    public Weapon nthWeapon(int number)
    {
        int nextIdx = playerData.PlayerActiveWeaponIdx;

        if (number >= 0 && number < playerData.PlayerWeapons.Count)
        {
            nextIdx = number % playerData.PlayerWeapons.Count;
            // playerData.PlayerWeapons[PlayerActiveWeaponIdx].Switch(); // TODO
            playerData.PlayerActiveWeaponIdx = nextIdx;
            // playerData.PlayerWeapons[PlayerActiveWeaponIdx].Switch(); // TODO
        }

        return playerData.PlayerWeapons[nextIdx];
    }

    
    // ----------------------------------------------------------------------------------------------------------------
    // INTERACTIONS
    // ----------------------------------------------------------------------------------------------------------------
    
    private void applyForce(float force, Vector2 direction)
    {
        Rigidbody2D playerRB = playerData.PlayerRB;

        playerRB.AddForce((direction.normalized * force) / playerData.MassMultiplier, ForceMode2D.Impulse);
    }

    public void takeDamage(int damage)
    {
        playerData.Health -= damage;
        if (playerData.Health <= 0)
        {
            Die();
        }
    }
    public void setHealth(int health)
    {
        playerData.Health = health;
    }

    public void Die()
    {
        PlayerDeath?.Invoke();
    }

    private void getHitByBullet(Bullet bullet)
    {

    }


    // OLD SWITCH WEAPON FUNCTION
    /*public void switchWeapons(char type, int number = 1)
    {
        // type == +, number == 1 -> switches to the next weapon
        // type == -, number == 2 -> switches to the prev prev weapon
        // type == =, number == 5 -> switches to the 5th weapon

        int boundary = playerData.PlayerWeapons.Count;
        
        if (boundary <= 0) { 
            return;             // No weapons to switch to -> return
        }

        int nextIdx = playerData.PlayerActiveWeaponIdx;

        switch (type)
        {
            case '+':
                nextIdx = (playerData.PlayerActiveWeaponIdx + number) % playerData.PlayerWeapons.Count;
                break;
            case '-':
                nextIdx = (playerData.PlayerActiveWeaponIdx - number) % playerData.PlayerWeapons.Count;
                break;
            case '=':
                if (number >= 0 && number < playerData.PlayerWeapons.Count)
                {
                    nextIdx = number % playerData.PlayerWeapons.Count;
                }
                break;
            default:
                break;
        }

        // playerData.PlayerWeapons[PlayerActiveWeaponIdx].Switch(); // TODO
        playerData.PlayerActiveWeaponIdx = nextIdx;
        // playerData.PlayerWeapons[PlayerActiveWeaponIdx].Switch(); // TODO
    }*/
}
