using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    [SerializeField] private GameObject minimap;
    [SerializeField] private GameObject enemyCount;

    private bool isMinimapEnabled = false;

    public void ToggleMinimap()
    {
        isMinimapEnabled = !isMinimapEnabled;
        minimap.SetActive(isMinimapEnabled);
        enemyCount.SetActive(isMinimapEnabled);
    }
}
