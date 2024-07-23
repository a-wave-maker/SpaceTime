using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRB;
    private Vector2 playerMoveDirection;

    [SerializeField] private float playerMassMultiplier = 1;
    [SerializeField] private float playerRotationSpeed = 1000;

    [SerializeField] private PlayerWeaponManager playerWeaponManager;
    [SerializeField] private PlayerWeapon playerDefaultWeapon;
    
    private List<PlayerWeapon> playerWeapons = new();

    private int playerLeftActiveWeaponIdx = 0;
    private int playerRightActiveWeaponIdx = 0;

    private int playerMaxHealth = 100;
    private int playerHealth = 100;

    private bool isSwitching = false;
    private int switchingIdx = 0;
    private bool switchingAccepted = false;

    public Rigidbody2D PlayerRB { get => playerRB; set => playerRB = value; }
    public Vector2 PlayerMoveDirection { get => playerMoveDirection; set => playerMoveDirection = value; }
    public List<PlayerWeapon> PlayerWeapons { get => playerWeapons; set => playerWeapons = value; }
    public int PlayerLeftActiveWeaponIdx { get => playerLeftActiveWeaponIdx; set => playerLeftActiveWeaponIdx = value; }
    public int PlayerRightActiveWeaponIdx { get => playerRightActiveWeaponIdx; set => playerRightActiveWeaponIdx = value; }
    public PlayerWeapon PlayerLeftActiveWeapon { get => playerWeapons[playerLeftActiveWeaponIdx]; }
    public PlayerWeapon PlayerRightActiveWeapon { get => playerWeapons[playerRightActiveWeaponIdx]; }
    public int PlayerHealth { get => playerHealth; set => playerHealth = value; }
    public float PlayerMassMultiplier { get => playerMassMultiplier; set => playerMassMultiplier = value; }
    public float PlayerRotationSpeed { get => playerRotationSpeed; set => playerRotationSpeed = value; }
    public int PlayerMaxHealth { get => playerMaxHealth; set => playerMaxHealth = value; }
    public bool IsSwitching { get => isSwitching; set => isSwitching = value; }
    public int SwitchingIdx { get => switchingIdx; set => switchingIdx = value; }
    public bool SwitchingAccepted { get => switchingAccepted; set => switchingAccepted = value; }

    // Start is called before the first frame update
    void Start()
    {
        Transform parent = transform;

        PlayerWeapon defaultWeapon = Instantiate(playerDefaultWeapon, parent);
        playerWeapons.Add(defaultWeapon);
        
        playerWeaponManager.InstantiateAllWeapons(parent);
        playerWeapons.AddRange(playerWeaponManager.InstantiatedWeapons);
        playerWeaponManager.DisableWeaponRendering();

        // turn on the first weapons
        PlayerLeftActiveWeapon.Switch();
        PlayerRightActiveWeapon.Switch();

    }

    public void AddWeapon(PlayerWeapon weapon)
    {
        playerWeapons.Add(weapon);
    }

    public void toggleSwitchingOn(int idx = 0)
    {
        isSwitching = true;
        switchingAccepted = false;
        switchingIdx = idx;
    }

    public void toggleSwitchingOff(int idx = 0) 
    {
        isSwitching = false;
        switchingAccepted = false;
        switchingIdx = idx;
    }

    public int acceptSwtiching(int idx = 0, bool left = true)
    {
        isSwitching = false;
        switchingAccepted = true;
        return idx;
    }

    public void updateSwitchingIdx(int valueChange)
    {
        switchingIdx += valueChange;
    }
}
