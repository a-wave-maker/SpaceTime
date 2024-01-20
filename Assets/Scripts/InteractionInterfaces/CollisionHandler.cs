using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))] // Ensure the object has a Collider component

public class CollisionHandler : MonoBehaviour
{
    [System.Serializable]
    public class CollisionSetting
    {
        public string layerName;
        public bool collideWithTag;
    }

    [SerializeField] private CollisionSetting[] collisionSettings = new CollisionSetting[]
    {
        new CollisionSetting{layerName = "Ignore Raycast", collideWithTag = false},

        new CollisionSetting{layerName = "Player", collideWithTag = true},
        new CollisionSetting{layerName = "PlayerBullet", collideWithTag = true},

        new CollisionSetting{layerName = "Enemy", collideWithTag = true},
        new CollisionSetting{layerName = "EnemyBullet", collideWithTag = true},

        new CollisionSetting{layerName = "Obstacle", collideWithTag = true},
    };

    void Start()
    {
        foreach (var setting in collisionSettings)
        {
            if (!setting.collideWithTag)
            {
                Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer(setting.layerName), true);
            }
        }
    }
}
