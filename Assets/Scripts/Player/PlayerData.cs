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
    private List<PlayerWeapon> playerWeapons = new List<PlayerWeapon>();
    private int playerActiveWeaponIdx = 0;

    private int playerMaxHealth = 100;
    private int playerHealth = 100;

    public Rigidbody2D PlayerRB { get => playerRB; set => playerRB = value; }
    public Vector2 PlayerMoveDirection { get => playerMoveDirection; set => playerMoveDirection = value; }
    public List<PlayerWeapon> PlayerWeapons { get => playerWeapons; set => playerWeapons = value; }
    public int PlayerActiveWeaponIdx { get => playerActiveWeaponIdx; set => playerActiveWeaponIdx = value; }
    public PlayerWeapon PlayerActiveWeapon { get => playerWeapons[playerActiveWeaponIdx]; }
    public int PlayerHealth { get => playerHealth; set => playerHealth = value; }
    public float PlayerMassMultiplier { get => playerMassMultiplier; set => playerMassMultiplier = value; }
    public float PlayerRotationSpeed { get => playerRotationSpeed; set => playerRotationSpeed = value; }
    public int PlayerMaxHealth { get => playerMaxHealth; set => playerMaxHealth = value; }

    // Start is called before the first frame update
    void Start()
    {
        /*PlayerWeapon defaultWeapon = new PlayerWeapon();    // TMP THROWS A WARNING
        defaultWeapon.FireRate = 0;             // TMP
        defaultWeapon.Recoil = 0;               // TMP
        playerWeapons.Add(defaultWeapon);
        playerWeapons.Add(playerActiveWeapon);  // TMP*/

        Transform parent = transform;

        PlayerWeapon defaultWeapon = Instantiate(playerDefaultWeapon, parent);
        playerWeapons.Add(defaultWeapon);
        
        playerWeaponManager.InstantiateAllWeapons(parent);
        playerWeapons.AddRange(playerWeaponManager.InstantiatedWeapons);
        playerWeaponManager.DisableWeaponRendering();

        PlayerWeapons[PlayerActiveWeaponIdx].Switch(); // turn on the first weapon
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addWeapon(PlayerWeapon weapon)
    {
        playerWeapons.Add(weapon);
    }

}
