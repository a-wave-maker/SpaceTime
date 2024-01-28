using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 offset = new (0f, 0f, -10f);

    [SerializeField] private float baseSize = 10f;
    [SerializeField] private float maxSize = 40f;
    [SerializeField] private float maxSpeed = 200f;
    [SerializeField] private float zoomSpeed = 1.2f;

    private bool cameraLock = false;

    private void LateUpdate()
    {
        if (target != null)
        {
            // follow the target
            transform.position = target.position + offset;

            // set the zoom
            ChangeSize();

            // set the offset
            SetPan();
        }
        else
        {
            Debug.LogError("Camera has no target");
        }
    }

    public void LockCameraPan()
    {
        cameraLock = true;
    }

    public void UnlockCameraPan()
    {
        cameraLock = false;
    }

    private void ChangeSize()
    {
        float differnce = GetGoalSize() - gameObject.GetComponent<Camera>().orthographicSize;

        gameObject.GetComponent<Camera>().orthographicSize += Time.deltaTime * differnce * zoomSpeed;

    }

    private float GetGoalSize()
    {
        float speedMultiplier = target.GetComponent<Rigidbody2D>().velocity.magnitude;

        float t = Mathf.Clamp01(speedMultiplier / maxSpeed);

        float newSize = Mathf.Lerp(baseSize, maxSize, t);

        return newSize;

    }

    private void SetPan()
    {
        Vector3 mousePosition = gameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPosition = target.position;

        Vector3 middlePoint = Vector3.Lerp(targetPosition, mousePosition, 0.3f);

        Vector3 difference = middlePoint - targetPosition;

        if (!cameraLock)
        {
            SetOffset(difference);
        }
    }

    private void SetOffset(Vector3 vector)
    {
        offset = vector;
        offset.z = -10f;
    }
}

