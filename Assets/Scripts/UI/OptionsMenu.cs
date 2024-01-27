using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject optionsUI;
    public Toggle bulletCollisionToggle;

    public void Start()
    {
        bulletCollisionToggle.isOn = true;
    }


    public void toggleBulletCollision()
    {
        // set/ignore collisions between bullet layers
        if (PlayerPrefs.GetInt("BulletCollision").Equals(0))
        {
            PlayerPrefs.SetInt("BulletCollision", 1);
            PlayerPrefs.Save();

            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("EnemyBullet"), false);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("PlayerBullet"), false);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("EnemyBullet"), false);

        } else
        {
            PlayerPrefs.SetInt("BulletCollision", 0);
            PlayerPrefs.Save();

            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("EnemyBullet"));
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("PlayerBullet"));
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("EnemyBullet"));

        }
    }


}
