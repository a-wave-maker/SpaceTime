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

    public void Fire(bool left)
    {
        int activeWeaponIndex = left ? playerData.PlayerLeftActiveWeaponIdx : playerData.PlayerRightActiveWeaponIdx;

        if (activeWeaponIndex == 0)
        {
            return;
        }
        // Fire weapon
        PlayerWeapon currentWeapon = playerData.PlayerWeapons[activeWeaponIndex];

        if (currentWeapon.Fire())
        {
            // Apply recoil
            float force = currentWeapon.Recoil;

            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0f;

            Vector2 direction = transform.position - targetPosition;

            ApplyForce(force, direction);
        }
    }

    public void FireLeft()
    {
        Fire(true);
    }

    public void FireRight()
    {
        Fire(false);
    }

    public void Reload()
    {
        playerData.PlayerLeftActiveWeapon.Reload();
        playerData.PlayerRightActiveWeapon.Reload();
    }


    // ----------------------------------------------------------------------------------------------------------------
    // WEAPON SWITCHING
    // ----------------------------------------------------------------------------------------------------------------

    private void SwitchWeapon(int fromIdx, int toIdx, bool left)
    {
        int otherWeaponIdx = left ? playerData.PlayerRightActiveWeaponIdx : playerData.PlayerLeftActiveWeaponIdx;

        // if the same weapon chosen, switch hands
        if (toIdx != 0 && toIdx == otherWeaponIdx)
        {
            playerData.PlayerLeftActiveWeaponIdx = 0;
            playerData.PlayerRightActiveWeaponIdx = 0;

            if (left)
            {
                playerData.PlayerLeftActiveWeaponIdx = toIdx;
                playerData.PlayerRightActiveWeaponIdx = fromIdx;
            } else
            {
                playerData.PlayerRightActiveWeaponIdx = toIdx;
                playerData.PlayerLeftActiveWeaponIdx = fromIdx;
            }
        } 
        else
        {
            playerData.PlayerWeapons[fromIdx].Switch();
            if (left)
            {
                playerData.PlayerLeftActiveWeaponIdx = toIdx;
            }
            else
            {
                playerData.PlayerRightActiveWeaponIdx = toIdx;
            }
            playerData.PlayerWeapons[toIdx].Switch();
        }

        // set the weapon to the right position
        playerData.PlayerWeapons[playerData.PlayerLeftActiveWeaponIdx].transform.localPosition = new Vector3(-0.35f, 0.3f, 0); // set the second value to -0.3f for a funny bug
        playerData.PlayerWeapons[playerData.PlayerRightActiveWeaponIdx].transform.localPosition = new Vector3(0.35f, 0.3f, 0);
    }

    public PlayerWeapon NextWeapon(bool left)
    {
        int activeWeaponIdx = left ? playerData.PlayerLeftActiveWeaponIdx : playerData.PlayerRightActiveWeaponIdx;

        int nextIdx = activeWeaponIdx;

        if (nextIdx == playerData.PlayerWeapons.Count - 1)
        {
            nextIdx = 0;
        }
        else
        {
            nextIdx = activeWeaponIdx + 1;
        }

        SwitchWeapon(activeWeaponIdx, nextIdx, left);

        return playerData.PlayerWeapons[nextIdx];
    }

    public PlayerWeapon PreviousWeapon(bool left)
    {
        int activeWeaponIdx = left ? playerData.PlayerLeftActiveWeaponIdx : playerData.PlayerRightActiveWeaponIdx;

        int nextIdx = left ? playerData.PlayerLeftActiveWeaponIdx : playerData.PlayerRightActiveWeaponIdx;

        if (nextIdx == 0)
        {
            nextIdx = playerData.PlayerWeapons.Count - 1;
        }
        else
        {
            nextIdx = activeWeaponIdx - 1;
        }

        SwitchWeapon(activeWeaponIdx, nextIdx, left);

        return playerData.PlayerWeapons[nextIdx];
    }

    public PlayerWeapon NthWeapon(int idx, bool left)
    {
        int activeWeaponIdx = left ? playerData.PlayerLeftActiveWeaponIdx : playerData.PlayerRightActiveWeaponIdx;

        int nextIdx = left ? playerData.PlayerLeftActiveWeaponIdx : playerData.PlayerRightActiveWeaponIdx;

        nextIdx = (idx % playerData.PlayerWeapons.Count + playerData.PlayerWeapons.Count) % playerData.PlayerWeapons.Count;

        SwitchWeapon(activeWeaponIdx, nextIdx, left);

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
