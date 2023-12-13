using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class SwitchingUI : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    private readonly List<Image> children = new();

    [SerializeField] private float baseWidth = 70;

    [SerializeField] private float middleOpacity = 1f;
    [SerializeField] private float betweenOpacity = 0.8f;
    [SerializeField] private float edgeOpacity = 0.3f;

    private bool isFading = false;
    private float fadeTimer = 0f;
    private float fadeTime = 0f;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject.GetComponent<Image>());
            SetMultipleImagesAlpha(children, 0f);
        }
    }

    private void Update()
    {
        // Can optimize the line below if needed
        // As it is now, it gets called every frame, it could be checked when
        // switching but that requires waiting for the player's switch function
        // to finish first
        UpdateElements();

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f || CheckNumericKeys()) // when buttons for weapon switching are pressed
        {
            // update the opacities and timers
            SetImageAlpha(GetComponent<Image>(), 1f);
            SetMultipleImagesAlpha(children, 1f);
            fadeTimer = 1f;
            fadeTime = 1f;
            isFading = false;

        }

        // update the fade process
        Fade();
    }

    private void UpdateElements()
    {
        List<Sprite> weaponSprites = GetSurroundingWeaponSprites(playerData.PlayerWeapons, playerData.PlayerActiveWeaponIdx, 5);

        if (weaponSprites.Count == children.Count)
        {
            for (int i = 0; i < children.Count; i++)
            {
                Image childImage = children[i];
                childImage.sprite = weaponSprites[i];
                SetOpacityAndSize(childImage, i);
            }
        }
    }

    private List<Sprite> GetSurroundingWeaponSprites(List<PlayerWeapon> originalList, int activeIndex, int itemCount)
    {
        if (originalList.Count == 0 || itemCount <= 0) return new List<Sprite>();

        List<Sprite> sublist = new();

        for (int i = activeIndex - (itemCount / 2); i <= activeIndex + (itemCount / 2); i++)
        {
            int wrappedIndex = (i % originalList.Count + originalList.Count) % originalList.Count;
            sublist.Add(originalList[wrappedIndex].SquareSprite);
        }

        sublist.Reverse();
        return sublist;
    }

    private void SetOpacityAndSize(Image image, int position)
    {
        float sizeMultiplier;

        if (position == 0 || position == children.Count - 1)
        {
            image.color = new Color(1f, 1f, 1f, edgeOpacity);
            sizeMultiplier = 0.3f;
        }
        else if (position == children.Count / 2)
        {
            image.color = new Color(1f, 1f, 1f, middleOpacity);
            sizeMultiplier = 1f;
        }
        else
        {
            image.color = new Color(1f, 1f, 1f, betweenOpacity);
            sizeMultiplier = 0.6f;
        }

        ScaleImage(image, sizeMultiplier);
    }

    private void ScaleImage(Image image, float sizeMultiplier)
    {
        Sprite sprite = image.sprite;
        float spriteAspectRatio = sprite.rect.width / sprite.rect.height;
        RectTransform imageRectTransform = image.GetComponent<RectTransform>();

        float newSize = baseWidth * spriteAspectRatio * sizeMultiplier;

        imageRectTransform.sizeDelta = new Vector2(newSize, baseWidth * sizeMultiplier);
    }

    private bool CheckNumericKeys()
    {
        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                return true;
            }
        }

        return false;
    }

    private void SetImageAlpha(Image image, float alpha)
    {
        Color currentColor = image.color;
        currentColor.a = alpha;
        image.color = currentColor;
    }

    private void SetMultipleImagesAlpha(List<Image> images, float alpha)
    {
        foreach (Image image in images)
        {
            SetImageAlpha(image, alpha);
        }
    }

    private void Fade()
    {
        if (fadeTimer <= 0f)
        {
            isFading = true;
        }

        if (isFading)
        {
            for (int i = 0; i < children.Count; i++)
            {
                Image image = children[i];
                if (i == 0 || i == children.Count - 1)
                {
                    SetImageAlpha(image, fadeTime * edgeOpacity);
                }
                else if (i == children.Count / 2)
                {
                    SetImageAlpha(image, fadeTime * middleOpacity);
                }
                else
                {
                    SetImageAlpha(image, fadeTime * betweenOpacity);
                }
            }
            SetImageAlpha(GetComponent<Image>(), fadeTime);
            fadeTime -= Time.deltaTime;
        }

        fadeTimer -= Time.deltaTime;
    }

}
