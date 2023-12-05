using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        // hide cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cursorScreenPos = Input.mousePosition;

        transform.position = cursorScreenPos;
    }
}
