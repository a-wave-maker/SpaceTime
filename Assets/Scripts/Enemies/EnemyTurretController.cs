using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretController : MonoBehaviour
{
    public Transform player;
    public GameObject Bullet;
    private bool moveable = false;
    private double mass = double.PositiveInfinity;
    private float fireRate = 2f;
    private float Damage = 69f;
    private float bulletForce = 69f;
    private float projectileSpeed = 0.1f;
    [SerializeField] private float rotateSpeed = 1f;


    public State currentState = State.Idle;

    void Start()
    {
        currentState = State.Idle;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case State.Idle:
                Patrol();
                Debug.Log("Patrolling...");
                break;
            case State.Attack:
                Debug.Log("Attacking!");
                break;
        }
    }
    
    void Attack(){

    }

    void Patrol(){
        // transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }
}



public enum State { Idle, Attack}
