using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] private Player player;

    [SerializeField] private CameraFollow playerCamera;

    public delegate void SuperHotMode();
    public static event SuperHotMode ChangeSuperHot;

    // Update is called once per frame
    void Update()
    {
        // DEBUG
        if (Input.GetKeyUp(KeyCode.K)) { // TMP
            player.Die();
        }
        if (Input.GetKeyUp(KeyCode.T)) { // TMP
            player.TakeDamage(8);
        }
        if (Input.GetKeyUp(KeyCode.H)) { // TMP
            player.Heal(7);
        }
        
        // GAME
        if (Input.GetKeyUp(KeyCode.Z))
        {
            ChangeSuperHot?.Invoke();
        }
        if (Input.GetKey(KeyCode.C))
        {
            playerCamera.LockCameraPan();
        }
        else
        {
            playerCamera.UnlockCameraPan();
        }

        // PLAYER
        if (Input.GetButtonDown("Fire1"))
        {
            player.Fire();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.Reload();
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) // Scroll Up
        {
            player.NextWeapon();
        }
        else if (scroll < 0f) // Scroll Down
        {
            player.PreviousWeapon();
        }

        // Switch weapon using number keys (1 to 9 and 0), 0 for default empty weapon
        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                player.NthWeapon(i);
            }
        }
    }
}
