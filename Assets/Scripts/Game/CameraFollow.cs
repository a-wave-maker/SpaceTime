using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);
    
    [SerializeField] private float baseSize = 20f;
    [SerializeField] private float maxSize = 40f;
    [SerializeField] private float panSpeed = 5f;
    [SerializeField] private float panSize = 20f;

    private void LateUpdate()
    {
        if (target != null)
        {
            // follow the target
            transform.position = target.position + offset;

            // set the zoom

            // set the offset
        }
        else
        {
            Debug.Log("Camera has no target");
        }
    }

    private void setSize()
    {

    }

    private void setPan()
    {
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        float panX = 0f;
        float panY = 0f;

        if (mouseX < 10f)
            panX = -1f;
        else if (mouseX > Screen.width - 10f)
            panX = 1f;

        if (mouseY < 10f)
            panY = -1f;
        else if (mouseY > Screen.height - 10f)
            panY = 1f;


    }
}

