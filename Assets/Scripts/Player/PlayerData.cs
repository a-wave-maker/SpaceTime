using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D playerRB;
    private float playerMoveSpeed;
    private Vector2 playerMoveDirection;

    private Weapon[] playerWeapons;
    private int playerActiveWeaponIndex;
    private Weapon playerActiveWeapon;


    public Rigidbody2D PlayerRB { get => playerRB; set => playerRB = value; }
    public float PlayerMoveSpeed { get => playerMoveSpeed; set => playerMoveSpeed = value; }
    public Vector2 PlayerMoveDirection { get => playerMoveDirection; set => playerMoveDirection = value; }
    public Weapon PlayerActiveWeapon { get => playerActiveWeapon; set => playerActiveWeapon = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
