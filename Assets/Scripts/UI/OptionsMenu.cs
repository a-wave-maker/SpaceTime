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
    public Toggle dynamicReloadingToggle;

    public void Start()
    {
        bulletCollisionToggle.isOn = true;
        dynamicReloadingToggle.isOn = true;
        if (PlayerPrefs.GetInt("BulletCollision").Equals(0))
        {
            toggleBulletCollision();
        }
        if (PlayerPrefs.GetInt("DynamicReloading").Equals(0))
        {
            toggleDynamicReloading();
        }
    }

    public void toggleBulletCollision()
    {
        // set/ignore collisions between bullet layers
        if (PlayerPrefs.GetInt("BulletCollision").Equals(0))
        {
            PlayerPrefs.SetInt("BulletCollision", 1);
            PlayerPrefs.Save();

            // dont ignore collisions between bullet layers
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("EnemyBullet"), false);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("PlayerBullet"), false);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("EnemyBullet"), false);

        } else
        {
            PlayerPrefs.SetInt("BulletCollision", 0);
            PlayerPrefs.Save();

            // ignore collisions between bullet layers
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("EnemyBullet"));
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("PlayerBullet"));
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("EnemyBullet"));

        }
    }

    public void toggleDynamicReloading()
    {
        if (PlayerPrefs.GetInt("DynamicReloading").Equals(1))
        {
            PlayerPrefs.SetInt("DynamicReloading", 0);
        } else
        {
            PlayerPrefs.SetInt("DynamicReloading", 1);
        }

    }


}
