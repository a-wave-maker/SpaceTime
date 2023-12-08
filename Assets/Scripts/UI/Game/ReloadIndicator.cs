using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadIndicator : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private int maxAmmo = 0;
    private int ammoLeft = 0;
    private bool isReloading = false;
    private float reloadProgress = 0;
    private bool canFire = true;

    private Image indicator = null;
    [SerializeField] private Color baseColor = new Color(0.5f, 0.9f, 1f, 1f);
    [SerializeField] private Color grayedOut = new Color(0.5f, 0.6f, 0.7f, 1f);

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
            indicator.fillAmount = 0.1f;
            return;
        }

        float clampedValue = 0.1f;

        if (isReloading)
        {
            indicator.color = grayedOut;
            clampedValue = 0.1f + (reloadProgress * (0.9f - 0.1f));
        }
        else
        {
            if (!canFire) {
                indicator.color = grayedOut;
            } else
            {
                indicator.color = baseColor;
            }
            float normalizedValue = 0.1f + 0.8f * (float)ammoLeft / maxAmmo;
            clampedValue = Mathf.Clamp(normalizedValue, 0.1f, 0.9f);
        }

        indicator.fillAmount = clampedValue;

    }

    private void UpdateData()
    {
        PlayerWeapon activeWeapon = playerData.PlayerActiveWeapon;

        isReloading = activeWeapon.IsReloading;
        reloadProgress = activeWeapon.ReloadProgress;
        maxAmmo = activeWeapon.MaxAmmo;
        ammoLeft = activeWeapon.RemainingAmmo;
        canFire = activeWeapon.CanFire();
    }
}
