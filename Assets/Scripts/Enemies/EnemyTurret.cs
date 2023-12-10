using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : DamageableEntity
{
    Rigidbody2D rigidbody;

    [SerializeField] public Transform target;
    [SerializeField] public GameObject bullet = null;
    [SerializeField] public bool moveable = false;
    [SerializeField] public double mass = double.PositiveInfinity;
    [SerializeField] public float fireRate = 2f;
    [SerializeField] public float Damage = 69f;
    [SerializeField] public float bulletForce = 69f;
    [SerializeField] public float projectileSpeed = 0.1f;
    [SerializeField] public float rotationSpeed = 60f;

    [SerializeField] public float viewRadius = 0f;
    [Range(0,360)]
    [SerializeField] public float viewAngle = 0f;



    public EnemyState currentState = EnemyState.Attack;

    protected override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        switch(currentState)
        {
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Idle:
                Patrol();
                break;
        }
    }
    
    void Attack(){
        LookAtTarget();
    }

    void Patrol(){
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.forward);
        
    }

    void LookAtTarget()
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle + 90f));

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }
}


public enum EnemyState { Idle, Attack}
