using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadIndicator : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private int maxAmmo = 0;
    private bool isReloading = false;
    private float reloadProgress = 0;
    private bool canFire = true;

    private Image indicator = null;
    [SerializeField] private Color baseColor = new (0.5f, 0.9f, 1f, 1f);
    [SerializeField] private Color grayedOut = new (0.5f, 0.6f, 0.7f, 1f);

    [SerializeField] private float minValue = 0f;
    [SerializeField] private float maxValue = 1f;

    // Start is called before the first frame update
    void Start()
    {
        indicator = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateData();
        if (maxAmmo == 0)
        {
            indicator.fillAmount = minValue;
            return;
        }

        float clampedValue;

        if (isReloading)
        {
            indicator.color = grayedOut;
            clampedValue = minValue + (reloadProgress * (maxValue - minValue));
        }
        else
        {
            if (!canFire) {
                indicator.color = grayedOut;
            } else
            {
                indicator.color = baseColor;
            }
            clampedValue = minValue + (maxValue - minValue) * playerData.PlayerActiveWeapon.RemainingAmmo / playerData.PlayerActiveWeapon.MaxAmmo;
        }

        indicator.fillAmount = clampedValue;

    }

    private void UpdateData()
    {
        PlayerWeapon activeWeapon = playerData.PlayerActiveWeapon;

        isReloading = activeWeapon.IsReloading;
        reloadProgress = activeWeapon.ReloadProgress;
        maxAmmo = activeWeapon.MaxAmmo;
        canFire = activeWeapon.CanFire();
    }
}
