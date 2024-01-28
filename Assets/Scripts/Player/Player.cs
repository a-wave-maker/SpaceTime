using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerData playerData;

    public delegate void PlayerDeathAction();
    public static event PlayerDeathAction PlayerDeath;

    public void Update()
    {
        if(playerData.PlayerHealth <= 0)
        {
            Die();
        }
    }

    public void OnGUI()
    {
        FaceCursor();
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
        PlayerWeapon currentWeapon = playerData.PlayerActiveWeapon;

        if (currentWeapon.Fire())
        {
            // Apply recoil
            float force = playerData.PlayerActiveWeapon.Recoil;

            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0f;

            Vector2 direction = transform.position - targetPosition;

            ApplyForce(force, direction);
        }
    }

    public void Reload()
    {
        playerData.PlayerActiveWeapon.Reload();
    }

    
    // ----------------------------------------------------------------------------------------------------------------
    // WEAPON SWITCHING
    // ----------------------------------------------------------------------------------------------------------------

    public void NextWeapon()
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

        playerData.PlayerWeapons[playerData.PlayerActiveWeaponIdx].Switch();
        playerData.PlayerActiveWeaponIdx = nextIdx;
        playerData.PlayerWeapons[playerData.PlayerActiveWeaponIdx].Switch();
    }

    public PlayerWeapon PreviousWeapon()
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

    public PlayerWeapon NthWeapon(int number)
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

    private void FaceCursor()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));

        // Determine the maximum degrees the rotation can change in one frame
        float maxDegreesPerFrame = playerData.PlayerRotationSpeed * Time.deltaTime;

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxDegreesPerFrame);
    }

    private void ApplyForce(float force, Vector2 direction)
    {
        Rigidbody2D playerRB = playerData.PlayerRB;

        playerRB.AddForce((direction.normalized * force) / playerData.PlayerMassMultiplier, ForceMode2D.Impulse);
    }

    public void TakeDamage(int damage)
    {
        playerData.PlayerHealth -= damage;
        if (playerData.PlayerHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int heal)
    {
        // Avoid overhealing
        if (heal > playerData.PlayerMaxHealth - playerData.PlayerHealth)
        {
            playerData.PlayerHealth = playerData.PlayerMaxHealth;
        } else
        {
            playerData.PlayerHealth += heal;
        }
    }
    public void SetHealth(int health)
    {
        playerData.PlayerHealth = health;
    }

    public void Die()
    {
        PlayerDeath?.Invoke();
        gameObject.SetActive(false);
    }

    public void TakeHit(float damage)
    {
        TakeDamage((int)damage);
    }

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
