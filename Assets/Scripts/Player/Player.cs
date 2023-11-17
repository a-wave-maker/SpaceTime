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

    public void Update()
    {
        if(playerData.PlayerHealth <= 0)
        {
            Die();
        }
    }

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
        playerData.PlayerActiveWeapon.Reload();
    }

    
    // ----------------------------------------------------------------------------------------------------------------
    // WEAPON SWITCHING
    // ----------------------------------------------------------------------------------------------------------------

    public void nextWeapon()
    {
        print(playerData.PlayerActiveWeaponIdx);
        print(playerData.PlayerWeapons.Count);

        int nextIdx = playerData.PlayerActiveWeaponIdx;
        if (nextIdx == playerData.PlayerWeapons.Count - 1)
        {
            nextIdx = 0;
        }
        else
        {
            nextIdx = playerData.PlayerActiveWeaponIdx + 1;
        }

        playerData.PlayerWeapons[playerData.PlayerActiveWeaponIdx].Switch();
        playerData.PlayerActiveWeaponIdx = nextIdx;
        playerData.PlayerWeapons[playerData.PlayerActiveWeaponIdx].Switch();
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

        playerData.PlayerWeapons[playerData.PlayerActiveWeaponIdx].Switch();
        playerData.PlayerActiveWeaponIdx = nextIdx;
        playerData.PlayerWeapons[playerData.PlayerActiveWeaponIdx].Switch();

        return playerData.PlayerWeapons[nextIdx];
    }

    public Weapon nthWeapon(int number)
    {
        int nextIdx = playerData.PlayerActiveWeaponIdx;

        if (number >= 0 && number < playerData.PlayerWeapons.Count)
        {
            nextIdx = number % playerData.PlayerWeapons.Count;
            playerData.PlayerWeapons[playerData.PlayerActiveWeaponIdx].Switch();
            playerData.PlayerActiveWeaponIdx = nextIdx;
            playerData.PlayerWeapons[playerData.PlayerActiveWeaponIdx].Switch();
        }

        return playerData.PlayerWeapons[nextIdx];
    }

    
    // ----------------------------------------------------------------------------------------------------------------
    // INTERACTIONS
    // ----------------------------------------------------------------------------------------------------------------
    
    private void applyForce(float force, Vector2 direction)
    {
        Rigidbody2D playerRB = playerData.PlayerRB;

        playerRB.AddForce((direction.normalized * force) / playerData.PlayerMassMultiplier, ForceMode2D.Impulse);
    }

    public void takeDamage(int damage)
    {
        playerData.PlayerHealth -= damage;
        if (playerData.PlayerHealth <= 0)
        {
            Die();
        }
    }
    public void setHealth(int health)
    {
        playerData.PlayerHealth = health;
    }

    public void Die()
    {
        PlayerDeath?.Invoke();
    }

/*    private Object getHit(Object @object)
    {
        // TODO Make it return the type it gets?

        if (@object.GetType == Bullet)
        {
            getHitByBullet(@object);
        }

        return null;
    }*/

/*    private void getHitByBullet(Bullet bullet)
    {
        takeDamage(bullet.Damage); // TODO to int
        // TODO calculate the angle
        applyForce(bullet.)
    }*/


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
