using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CollisionManager;

[RequireComponent(typeof(Collider2D))] // Ensure the object has a Collider component

public class CollisionManager : MonoBehaviour
{

    // This is just so that the settings are possible to change in the inspector
    [System.Serializable]
    public struct collisionSetting
    {
        public string layerName;
        public bool ignore;
    }

    [SerializeField]
    private collisionSetting[] collisionSettings = new collisionSetting[]
    {
        new collisionSetting { layerName="Player", ignore=false},
        new collisionSetting { layerName="PlayerBullet", ignore=false},
        new collisionSetting { layerName="Enemy", ignore=false},
        new collisionSetting { layerName="EnemyBullet", ignore=false},
        new collisionSetting { layerName="Obstacle", ignore=false},
    };

    // With which LAYERS THIS OBJECT IGNORES collisions with
    // The default layers are here just in case, they generally shouldn't be necessary to change
    private Dictionary<string, bool> ignoreCollisionsWith = new Dictionary<string, bool>
    {
       // Layer Name            Ignore?
        { "Default",            false },
        { "TransparentFX",      false },
        { "Ignore Raycast",     false },
        { "Water",              false },
        { "UI",                 false },

        { "Player",             false },
        { "PlayerBullet",       false },
        { "Enemy",              false },
        { "EnemyBullet",        false },
        { "Obstacle",           false }
    };

    private void Start()
    {
        BoxCollider2D defaultBoundingBox = gameObject.GetComponent<BoxCollider2D>();
        
        if (defaultBoundingBox == null)
        {
            Debug.LogWarning("You are seeing this warning because CollisionHandler requires a bounding box (an additional collider that is a trigger) " +
                             "and by default that is a BoxCollider2D. " +
                             "If your bounding box is of different type you can ignore this warning. " +
                             "Otherwise know that the dynamic collisions will work BUT will not work correctly.");
        }

        // Apply initial settings
        foreach (var collisionSetting in collisionSettings) 
        {
            ignoreCollisionsWith[collisionSetting.layerName] = collisionSetting.ignore;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        string collidedLayer = LayerMask.LayerToName(collision.gameObject.layer);


        if (!ignoreCollisionsWith.ContainsKey(collidedLayer))
        {
            Debug.LogError($"Collided with unsupported layer: {collidedLayer}. Make sure the layer is correct or add the layer to the CollisionHandler");

            return;
        }

        // If collision allowed, run the function
        if (ignoreCollisionsWith[collidedLayer])
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>(), true);
        }
    }

}
