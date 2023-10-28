using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the player's Transform component

    public Vector3 offset = new Vector3(0f, 0f, -10f); // Offset between the camera and the player

    void LateUpdate()
    {
        if (target != null)
        {
            // Set the camera's position to the player's position plus the offset
            transform.position = target.position + offset;
        }
    }
}
