using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransfrom;
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private Vector3 offset = new Vector3(0f, 0f, 0f);
    [SerializeField]
    private float baseAlpha = 0.1f;

    private Camera mainCamera;
    private Image indicator = null;

    private void Start()
    {
        indicator = GetComponent<Image>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        float normalizedValue = 0.1f + 0.8f * playerData.PlayerHealth / playerData.PlayerMaxHealth;
        float clampedValue = Mathf.Clamp(normalizedValue, 0.1f, 0.9f);
        indicator.fillAmount = clampedValue;

        float multiplier = 1 / ((clampedValue + 1) / 2);

        // Update the alpha
        // It has to be done with a tmp. You can only replace the color, you cannot update it
        Color tmp = indicator.color;
        tmp.a = baseAlpha * Mathf.Pow(multiplier, 10);
        indicator.color = tmp;
    }
}
