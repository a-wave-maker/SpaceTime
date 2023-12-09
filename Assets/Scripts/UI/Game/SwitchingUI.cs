using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchingUI : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private List<GameObject> children = new List<GameObject>();
    [SerializeField] private float baseWidth = 70;
    [SerializeField] private float baseHeight = 70;

    void Start()
    {
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }
    }

    void Update()
    {
        List<Sprite> weaponSprites = GetSurroundingSprites(playerData.PlayerWeapons, playerData.PlayerActiveWeaponIdx, 5);

        if (weaponSprites.Count == children.Count)
        {
            // Loop through each small image holder and assign a sprite
            for (int i = 0; i < children.Count; i++)
            {
                Image childImage = children[i].GetComponent<Image>();

                // Set the sprite
                childImage.sprite = weaponSprites[i];

                // Scale the small image to fit its aspect ratio
                SetOpacityAndSize(childImage, i);
            }
        }
        else
        {
            Debug.LogError("Number of sprites to assign does not match the number of small image holders.");
        }
    }

    void SetOpacityAndSize(Image image, int position)
    {
        float middleOpacity = 1f;
        float betweenOpacity = 0.75f;
        float edgeOpacity = 0.2f;
        float sizeMultiplier = 1f;

        // Set opacity
        if (position == 0 || position == children.Count - 1)
        {
            image.color = new Color(1f, 1f, 1f, edgeOpacity);
            sizeMultiplier = 0.5f;
        }
        else if (position == children.Count / 2)
        {
            image.color = new Color(1f, 1f, 1f, middleOpacity);
        }
        else
        {
            image.color = new Color(1f, 1f, 1f, betweenOpacity);
            sizeMultiplier = 0.75f;
        }

        // Set size based on the original aspect ratio
        FitImageToAspectRatio(image, sizeMultiplier);
    }

    // Scale the image to fit its aspect ratio
    void FitImageToAspectRatio(Image image, float sizeMultiplier)
    {
        Sprite sprite = image.sprite;
        float spriteAspectRatio = sprite.rect.width / sprite.rect.height;
        RectTransform imageRectTransform = image.GetComponent<RectTransform>();
        float imageAspectRatio = imageRectTransform.rect.width / imageRectTransform.rect.height;

        if (imageAspectRatio != spriteAspectRatio)
        {
            float newSize = baseWidth * spriteAspectRatio * sizeMultiplier;

            imageRectTransform.sizeDelta = new Vector2(newSize, baseWidth * sizeMultiplier);
        }
    }

    public List<Sprite> GetSurroundingSprites(List<PlayerWeapon> originalList, int activeIndex, int itemCount)
    {
        if (originalList.Count == 0 || itemCount <= 0)
        {
            // This list should never be empty but just in case
            Debug.Log("How did you drop the 'nothing' weapon?? Where are your weapons mate???");
            return new List<Sprite>();
        }

        List<Sprite> sublist = new List<Sprite>();

        // Add items behind active index
        for (int i = activeIndex - (itemCount / 2); i < activeIndex; i++)
        {
            int wrappedIndex = WrapIndex(i, originalList.Count);
            sublist.Add(originalList[wrappedIndex].GetComponent<SpriteRenderer>().sprite);
        }

        // Add the item at the active index
        int wrappedActiveIndex = WrapIndex(activeIndex, originalList.Count);
        sublist.Add(originalList[wrappedActiveIndex].GetComponent<SpriteRenderer>().sprite);

        // Add items in front of active index
        for (int i = activeIndex + 1; i <= activeIndex + (itemCount / 2); i++)
        {
            int wrappedIndex = WrapIndex(i, originalList.Count);
            sublist.Add(originalList[wrappedIndex].GetComponent<SpriteRenderer>().sprite);
        }

        sublist.Reverse();

        return sublist;
    }

    // Helper method to wrap indices around the list boundaries
    private int WrapIndex(int index, int listCount)
    {
        if (listCount == 0)
        {
            return 0;
        }

        return (index % listCount + listCount) % listCount;
    }
}
