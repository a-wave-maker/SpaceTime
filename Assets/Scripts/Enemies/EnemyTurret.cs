using UnityEngine;
using UnityEngine.Events;

public class EnemyTurret : DamageableEnemy
{
    private enum EnemyState { Idle, Combat }

    private Transform target;
    // private bool moveable = false; // TODO
    // private double mass = double.PositiveInfinity; // TODO
    [SerializeField] private EnemyWeapon weapon = null;
    [SerializeField] private float rotationSpeed = 60f;

    // [SerializeField] private float viewRadius = 0f;
    // [Range(0,360)]
    // [SerializeField] private float viewAngle = 0f;
    [SerializeField] private float range = 20f;

    [SerializeField] private EnemyState currentState = EnemyState.Combat;

    protected override void Start()
    {
        base.Start();

        // target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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

        LayerMask layerToIgnore = LayerMask.GetMask("Enemy");

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.up), range, ~layerToIgnore);


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
       // Vector3 direction = target.position - transform.position;
       // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
       // Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));

       // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }

    
}


