using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public void OnButtonClick()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null)
        {
            gameManager.StartGame("Level1");
        }
        else
        {
            Debug.Log("Couldn't find a GameManager");
        }
    }
}
