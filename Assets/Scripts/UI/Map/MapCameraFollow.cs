using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -5);

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
