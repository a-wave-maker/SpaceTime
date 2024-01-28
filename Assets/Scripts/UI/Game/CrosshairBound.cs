using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairBound : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new (0f, 0f, 0f);

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
        Vector3 cursorScreenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorScreenPos.z = 0;

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.position = cursorScreenPos + offset;
        transform.position = cursorScreenPos + offset;
    }
}
