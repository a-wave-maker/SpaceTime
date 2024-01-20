using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))] // Ensure the object has a Collider component

public abstract class ICollidable : MonoBehaviour
{
    [SerializeField] public abstract void OnCollisionConfirmed(Collision2D collision);

    // With which LAYERS THIS OBJECT IGNORES collisions with
    [SerializeField] private Dictionary<string, bool> ignoreCollisionsWith = new Dictionary<string, bool> 
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        string collidedLayer = LayerMask.LayerToName(collision.gameObject.layer);

        if (!ignoreCollisionsWith.ContainsKey(collidedLayer))
        {
            Debug.LogError($"Collided with unsupported layer: {collidedLayer}. Make sure the layer is correct or add the layer to the CollisionHandler");

            return;
        }

        // If collision allowed, run the function
        if (!ignoreCollisionsWith[collidedLayer])
        {
            OnCollisionConfirmed(collision);
        }
    }


}
