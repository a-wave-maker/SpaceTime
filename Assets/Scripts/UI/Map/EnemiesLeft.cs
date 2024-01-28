using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesLeft : MonoBehaviour
{
    private GameData gameData;
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameManager>().GetComponent<GameData>();
        text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = ("Enemies Left: " + gameData.EnemyCount.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        text.text = ("Enemies Left: " + gameData.EnemyCount.ToString());
    }
}
