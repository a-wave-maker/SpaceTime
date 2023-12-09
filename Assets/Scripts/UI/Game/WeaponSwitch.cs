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
        FitImageToAspectRatio(currentWeapon);

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

/*public class ImageLayoutController : MonoBehaviour
{
    public Image bigImage;
    public GameObject[] smallImageHolders;

    void Start()
    {
        // Assuming you have sprites to assign (replace these with your own sprites)
        Sprite[] spritesToAssign = LoadSpritesDynamically();

        if (spritesToAssign.Length == smallImageHolders.Length)
        {
            // Loop through each small image holder and assign a sprite
            for (int i = 0; i < smallImageHolders.Length; i++)
            {
                Image smallImage = smallImageHolders[i].GetComponent<Image>();

                // Set the sprite
                smallImage.sprite = spritesToAssign[i];

                // Scale the small image to fit its aspect ratio
                FitImageToAspectRatio(smallImage);
            }
        }
        else
        {
            Debug.LogError("Number of sprites to assign does not match the number of small image holders.");
        }
    }

    // Replace this method with your own logic to load sprites dynamically
    Sprite[] LoadSpritesDynamically()
    {
        // Your logic to load sprites dynamically goes here
        // Return an array of sprites
        return new Sprite[5];
    }

    // Scale the image to fit its aspect ratio
    void FitImageToAspectRatio(Image image)
    {
        if (image.sprite != null)
        {
            float aspectRatio = image.sprite.rect.width / image.sprite.rect.height;
            image.SetNativeSize();
            image.rectTransform.sizeDelta = new Vector2(image.rectTransform.sizeDelta.y * aspectRatio, image.rectTransform.sizeDelta.y);
        }
    }
}*/