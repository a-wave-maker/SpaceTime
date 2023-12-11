using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class EnemyTurret : DamageableEntity
{
    public enum EnemyState { Idle, Combat }


    Rigidbody2D rigidbody;

    [SerializeField] public Transform target;
    [SerializeField] public EnemyWeapon weapon = null;
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


    public EnemyState currentState = EnemyState.Combat;



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
            case EnemyState.Combat:
                Combat();
                break;
            case EnemyState.Idle:
                Patrol();
                break;
        }
    }
    
    void Combat(){
        LookAtTarget();
        Attack();
    }

    void Patrol(){
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.forward);
        
    }

    void Attack()
    {

        LayerMask enemyMask = LayerMask.GetMask("Enemies");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.up));


        if (hit.collider != null)
        {
            if(hit.collider.gameObject.name == "Player")
            {
                weapon.Fire();
            }
        }


    }

    void LookAtTarget()
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }

    
}


