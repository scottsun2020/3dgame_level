using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour {
    public Transform[] points;
    private int destPoint = 0;

    // Wait time in seconds
    // private float _waitTime = 1f; 
    // private float _waitCounter = 0f;
    // private bool _waiting = false;

    // Define the enemy's look radius
    public float lookRadius = 10f;
    public float projectileRadius = 25f;


    // Define boolean variables that enforce our enemy's attack cooldown
    public bool CanAttack = true;
    public float AttackCooldown = 2.0f;
    public float AttackCooldown2 = 3.0f;
    public bool isAttacking = false;

    // This is a reference to the enemy projectile prefab
    public GameObject m_Projectile;

    // This is a reference to the transform where the enemy projectile prefab will spawn
    public Transform m_SpawnTransform;

    private Animator anim;
    Transform target;
    NavMeshAgent agent;

    GameObject weapon;
    BoxCollider colliderWeapon;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        weapon = GameObject.Find("Routh1.R");
        colliderWeapon = weapon.GetComponent<BoxCollider>();
        colliderWeapon.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        float waypointDistance = Vector3.Distance(points[destPoint].position, transform.position);
        anim.SetBool("IsMoving", false);

        // Conditional statement checks if the distance between the enemy and player is less than or equal to the enemy's projectile and look radius
        if (distance <= projectileRadius  && distance > lookRadius) {
            anim.SetBool("IsMoving", false);
            // Attack the target
            if (CanAttack)
                Attack2();

            // Face the target
            FaceTarget(target.position);
        }     
        else if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            anim.SetBool("IsMoving", true);

            if (distance <= agent.stoppingDistance)
            {
                anim.SetBool("IsMoving", false);

                // Attack the target
                if (CanAttack)
                    Attack();

                // Face the target
                FaceTarget(target.position);
            }
        }
        else
        {
            // Returns if no points have been set up
            if (points.Length == 0)
                return;

            // Debug.Log(destPoint);

            // Set the agent to go to the currently selected destination
            agent.SetDestination(points[destPoint].position);
            anim.SetBool("IsMoving", true);

            if (waypointDistance <= agent.stoppingDistance)
            {
                anim.SetBool("IsMoving", false);

                // Choose the next point in the array as the destination, cycling to the start if necessary
                destPoint = (destPoint + 1) % points.Length;

                // Face the target
                FaceTarget(points[destPoint].position);
            }
        }
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination
        agent.SetDestination(points[destPoint].position);

        // Choose the next point in the array as the destination, cycling to the start if necessary
        destPoint = (destPoint + 1) % points.Length;
    }

    public void Attack()
    {
        isAttacking = true;
        CanAttack = false;
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }

    public void Attack2()
    {
        isAttacking = true;
        CanAttack = false;
        GameObject projectile = Instantiate(m_Projectile, m_SpawnTransform.position, m_SpawnTransform.rotation);
        StartCoroutine(ResetAttack2Cooldown());
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

    IEnumerator ResetAttack2Cooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown2);
        CanAttack = true;
    }
    public void EnableAttack()
    {
        colliderWeapon.enabled = true;
    }

    public void DisableAttack()
    {
        colliderWeapon.enabled = false;
    }

    void FaceTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Display in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, projectileRadius);
    }
}
