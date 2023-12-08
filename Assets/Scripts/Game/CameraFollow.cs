using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public Vector3 offset = new Vector3(0f, 0f, -10f);

    void LateUpdate()
    {
        if (target != null)
        {
            // Set the camera's position to the target's position plus the offset
            transform.position = target.position + offset;
        } else
        {

        }
    }
}
