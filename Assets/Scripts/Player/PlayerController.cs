using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] private Player player;

    [SerializeField] private CameraController playerCamera;

    [SerializeField] private MinimapManager minimapManager;

    [SerializeField] private PauseMenu pauseMenu;

    private GameManager gameManager;
    
    private SuperHotManager superHotManager;

    public override void OnNetworkSpawn()
    {
        if(!IsOwner) return;
        playerCamera = GetComponent<CameraController>();
    
    }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        superHotManager = gameManager.GetComponent<SuperHotManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
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
                player.Fire();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                if(gameManager.currentState == GameManager.GameState.Playing)
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
}
