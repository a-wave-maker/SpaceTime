using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Transform playerTransfrom;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, 0f);
    [SerializeField] private float baseAlpha = 0.1f;

    [SerializeField] private float minValue = 0f;
    [SerializeField] private float maxValue = 1f;

    private Image indicator = null;

    private bool fade = false;

    private void Start()
    {
        indicator = GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
        float normalizedValue = minValue + (maxValue - minValue) * playerData.PlayerHealth / playerData.PlayerMaxHealth;
        float clampedValue = Mathf.Clamp(normalizedValue, minValue, maxValue);
        indicator.fillAmount = clampedValue;

        float multiplier = 1 / ((clampedValue + 1) / 2);

        // Update the alpha
        // It has to be done with a tmp. You can only replace the color, you cannot update it
        if (playerData.PlayerHealth > playerData.PlayerMaxHealth / 2)
        {
            Color tmp = indicator.color;
            tmp.a = baseAlpha * Mathf.Pow(multiplier, 10);
            indicator.color = tmp;
        } else
        {
            Flash();
        }

    }

    private void Flash()
    {
        Color tmp = indicator.color;

        if (indicator.color.a <= 0.2f)
        {
            fade = true;
        } else if (indicator.color.a >= 0.99f) {

            fade = false;
        }

        if(fade)
        {
            // fade in
            tmp.a += 2 * Time.deltaTime;
        } else
        {
            // fade out
            tmp.a -= 2 * Time.deltaTime;
        }
        

        indicator.color = tmp;

    }
}
