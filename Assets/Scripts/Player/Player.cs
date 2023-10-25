using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        Vector2 direction = playerData.PlayerMoveDirection;
        Weapon currentWeapon = playerData.PlayerActiveWeapon;
        currentWeapon.Fire(direction);
    }

    public void Reload()
    {
        // TODO
    }
}
