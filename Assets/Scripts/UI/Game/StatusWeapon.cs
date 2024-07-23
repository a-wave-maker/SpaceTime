using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Image leftMask;
    [SerializeField] private Image rightMask;
    [SerializeField] private Image leftWeapon;
    [SerializeField] private Image rightWeapon;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        leftWeapon.sprite = playerData.PlayerLeftActiveWeapon.SquareSprite;
        rightWeapon.sprite = playerData.PlayerRightActiveWeapon.SquareSprite;
    }
}
