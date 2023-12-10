using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    Rigidbody2D rigidbody;

    [SerializeField] public Transform target;
    [SerializeField] public GameObject Bullet;
    [SerializeField] public bool moveable = false;
    [SerializeField] public double mass = double.PositiveInfinity;
    [SerializeField] public float fireRate = 2f;
    [SerializeField] public float Damage = 69f;
    [SerializeField] public float bulletForce = 69f;
    [SerializeField] public float projectileSpeed = 0.1f;
    [SerializeField] public float rotateSpeed = 60f;

    [SerializeField] public float viewRadius = 0f;
    [Range(0,360)]
    [SerializeField] public float viewAngle = 0f;



    public EnemyState currentState = EnemyState.Idle;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("player").GetComponent<Transform>();
        currentState = EnemyState.Idle;
        Vector2 direction = (target.position - transform.position).normalized;  
    }

    void Update()
    {
        switch(currentState)
        {
            case EnemyState.Idle:
                Patrol();
                break;
            case EnemyState.Attack:
                break;
        }
    }
    
    void Attack(){

    }

    void Patrol(){
        transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.forward);
        
    }

}


public enum EnemyState { Idle, Attack}
