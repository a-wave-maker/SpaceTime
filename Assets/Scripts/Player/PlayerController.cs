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
        if (Input.GetKeyUp(KeyCode.K)) { // TMP
            player.Die();
        }

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
            player.nextWeapon();
        }
        else if (scroll < 0f) // Scroll Down
        {
            player.previousWeapon();
        }

        // Switch weapon using number keys (1 to 9 and 0), 0 for default empty weapon
        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                player.nthWeapon(i);
            }
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
