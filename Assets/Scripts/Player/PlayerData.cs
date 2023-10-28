using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D playerRB;
    private float playerMoveSpeed;
    private Vector2 playerMoveDirection;

    private List<Weapon> playerWeapons = new List<Weapon>();
    private int playerActiveWeaponIdx = 0;
    [SerializeField] // TMP
    private Weapon playerActiveWeapon; // TMP


    public Rigidbody2D PlayerRB { get => playerRB; set => playerRB = value; }
    public float PlayerMoveSpeed { get => playerMoveSpeed; set => playerMoveSpeed = value; }
    public Vector2 PlayerMoveDirection { get => playerMoveDirection; set => playerMoveDirection = value; }
    public List<Weapon> PlayerWeapons { get => playerWeapons; set => playerWeapons = value; }
    public int PlayerActiveWeaponIdx { get => playerActiveWeaponIdx; set => playerActiveWeaponIdx = value; }
    public Weapon PlayerActiveWeapon { get => playerWeapons[playerActiveWeaponIdx]; }

    // Start is called before the first frame update
    void Start()
    {
        Weapon defaultWeapon = new Weapon();    // TMP THROWS A WARNING
        defaultWeapon.FireRate = 0;             // TMP
        defaultWeapon.Recoil = 0;               // TMP
        playerWeapons.Add(defaultWeapon);
        playerWeapons.Add(playerActiveWeapon);  // TMP
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addWeapon(Weapon weapon)
    {
        playerWeapons.Add(weapon);
    }
}
