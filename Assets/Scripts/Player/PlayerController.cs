using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private Player player;

    public delegate void SuperHotMode();
    public static event SuperHotMode ChangeSuperHot;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnGUI()
    {
        faceCursor();
    }

    // Update is called once per frame
    void Update()
    {
        // DEBUG
        if (Input.GetKeyUp(KeyCode.K)) { // TMP
            player.Die();
        }
        if (Input.GetKeyUp(KeyCode.T)) { // TMP
            player.takeDamage(8);
        }
        if (Input.GetKeyUp(KeyCode.H)) { // TMP
            player.Heal(7);
        }
        
        // GAME
        if (Input.GetKeyUp(KeyCode.Z))
        {
            ChangeSuperHot?.Invoke();
        }

        // PLAYER
        if (Input.GetButtonDown("Fire1"))
        {
            player.Fire();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.Reload();
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
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));

        // Determine the maximum degrees the rotation can change in one frame
        float maxDegreesPerFrame = playerData.PlayerRotationSpeed * Time.deltaTime;

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxDegreesPerFrame);
    }
}
