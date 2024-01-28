using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperHotManager : MonoBehaviour
{
    private PlayerData playerData;

    private bool modeSuperHot = false;
    [SerializeField] private float superHotMin = 0;
    [SerializeField] private float superHotMax = 50;
    
    public bool ModeSuperHot { get => modeSuperHot; set => modeSuperHot = value; }

    // Update is called once per frame
    void Update()
    {
        if (modeSuperHot)
        {
            SetTimeScale(MappedSpeed() + 0.1f);
        }
    }

    private float MappedSpeed()
    {
        if (playerData != null)
        {
            float clampedSpeed = Mathf.Clamp(playerData.PlayerRB.velocity.magnitude, superHotMin, superHotMax); // get time change from current player speed

            float mappedValue = Mathf.Lerp(0f, 0.9f, clampedSpeed / superHotMax);

            return mappedValue;
        } 
        else 
        {
            Debug.Log("PlayerData script not found");
            return 0.9f;
        }
    }

    public void SetTimeScale(float amount)
    {
        Time.timeScale = amount;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }

    public void ToggleSuperHot()
    {
        modeSuperHot = !modeSuperHot;
    }
}
