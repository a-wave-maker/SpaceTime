using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadIndicator : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    private int maxAmmo = 0;
    private int ammoLeft = 0;
    private bool isReloading = false;
    private float reloadProgress = 0;

    private Image indicator = null;

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
            clampedValue = 0.1f + (reloadProgress * (0.9f - 0.1f));
        }
        else
        {
            float normalizedValue = 0.1f + 0.8f * (float)ammoLeft / maxAmmo;
            clampedValue = Mathf.Clamp(normalizedValue, 0.1f, 0.9f);
        }

        indicator.fillAmount = clampedValue;

    }

    private void UpdateData()
    {
        isReloading = playerData.PlayerActiveWeapon.IsReloading;
        reloadProgress = playerData.PlayerActiveWeapon.ReloadProgress;
        maxAmmo = playerData.PlayerActiveWeapon.MaxAmmo;
        ammoLeft = playerData.PlayerActiveWeapon.RemainingAmmo;
    }
}
