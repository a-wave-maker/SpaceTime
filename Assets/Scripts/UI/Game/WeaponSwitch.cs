using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{
    private Image prevPrevWeapon;
    private Image prevWeapon;
    private Image currentWeapon;
    private Image nextWeapon;
    private Image nextNextWeapon;

    [SerializeField] private PlayerData playerData;

    private bool switching = false;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = GameObject.Find("/UICanvas/Weapons/CurrentWeapon").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // update weapons
        currentWeapon.sprite = playerData.PlayerActiveWeapon.GetComponent<SpriteRenderer>().sprite;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f) { switching = true; }

        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString())) { switching = true; }
        }

        if (switching)
        {
            // show stuff
        }
    }

    void FitImageToAspectRatio(Image image)
    {
        if (image.sprite != null)
        {
            float aspectRatio = image.sprite.rect.width / image.sprite.rect.height;
            image.SetNativeSize();
            image.rectTransform.sizeDelta = new Vector2(image.rectTransform.sizeDelta.y * aspectRatio, image.rectTransform.sizeDelta.y);
        }
    }
}
