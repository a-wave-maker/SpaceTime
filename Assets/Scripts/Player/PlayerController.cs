using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] private Player player;

    [SerializeField] private CameraFollow playerCamera;

    [SerializeField] private MinimapManager minimapManager;

    [SerializeField] private PauseMenu pauseMenu;

    private GameManager gameManager;
    
    private SuperHotManager superHotManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        superHotManager = gameManager.GetComponent<SuperHotManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameManager.currentState == GameManager.GameState.Playing || gameManager.currentState == GameManager.GameState.Paused)
            {
                if (pauseMenu.isGamePaused)
                {
                    pauseMenu.Resume();
                }
                else
                {
                    pauseMenu.Pause();
                }
            }
        }

        if (gameManager.currentState == GameManager.GameState.Playing)
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
                superHotManager.ToggleSuperHot();
            }
            if (Input.GetKey(KeyCode.C))
            {
                playerCamera.LockCameraPan();
            }
            else
            {
                playerCamera.UnlockCameraPan();
            }
            if (Input.GetKeyUp(KeyCode.M))
            {
                minimapManager.ToggleMinimap();
            }

            // PLAYER
            if (Input.GetButton("Fire1"))
            {
                if (playerData.IsSwitching)
                {
                    switchWeapon(playerData.SwitchingIdx, true);
                    playerData.acceptSwtiching();
                }
                else
                {
                    player.Fire(true);
                }
            }
            if (Input.GetButton("Fire2"))
            {
                if (playerData.IsSwitching)
                {
                    switchWeapon(playerData.SwitchingIdx, false);
                    playerData.acceptSwtiching();
                }
                else
                {
                    player.Fire(false);
                }
            }
            if (Input.GetButton("Fire3"))
            {
                if (playerData.IsSwitching)
                {
                    playerData.toggleSwitchingOff();
                } else
                {
                    playerData.toggleSwitchingOn();
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if(gameManager.currentState == GameManager.GameState.Playing)
                    player.Reload();
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f) // Scroll Up
            {
                if (!playerData.IsSwitching)
                {
                    playerData.toggleSwitchingOn();
                }
                playerData.updateSwitchingIdx(1);
            }
            else if (scroll < 0f) // Scroll Down
            {
                if (!playerData.IsSwitching)
                {
                    playerData.toggleSwitchingOn();
                }
                playerData.IsSwitching = true;
                playerData.updateSwitchingIdx(-1);
            }

            // Switch weapon using number keys (1 to 9 and 0), 0 for default empty weapon
            for (int i = 0; i <= 9; i++)
            {
                if (Input.GetKeyDown(i.ToString()))
                {
                    playerData.toggleSwitchingOn();
                    playerData.updateSwitchingIdx(i);
                }
            }
        }
    }

    private void switchWeapon(int idx, bool left)
    {
        int weaponIdx = idx % playerData.PlayerWeapons.Count;
        player.NthWeapon(weaponIdx, left);
    }
}
