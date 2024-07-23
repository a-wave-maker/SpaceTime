using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timer;
    private GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<TextMeshProUGUI>();
        gameData = GameObject.Find("GameManager").GetComponent<GameData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer != null && gameData != null)
        {
            float t = gameData.TimePassed / 2;

            int hours = (int)(t / 3600);
            int minutes = (int)((t % 3600) / 60);
            int seconds = (int)(t % 60);
            int milliseconds = (int)((t - Mathf.Floor(t)) * 100);

            string timeString = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", hours, minutes, seconds, milliseconds);


            timer.text = timeString;
        }
    }
}
