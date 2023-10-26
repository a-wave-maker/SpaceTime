using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private Player player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        faceCursor();

        if (Input.GetButtonDown("Fire1"))
        {
            player.Fire();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.Reload(); // TODO
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) // Scroll Up
        {
            player.switchWeapons('+');
        }
        else if (scroll < 0f) // Scroll Down
        {
            player.switchWeapons('-');
        }

        // Switch weapon using number keys (1 to 9) and 0 for default empty weapon
        for (int i = 1; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                player.switchWeapons('=', i - 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            player.switchWeapons('=', 0);
        }

    }

    private void faceCursor()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));
    }
}
