using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    // Define a look radius
    public float lookRadius = 10f;

    // Define boolean variables that enforce our enemy's attack cooldown
    public bool CanAttack = true;
    public float AttackCooldown = 2.0f;
    public bool isAttacking = false;
    
    private Animator anim;
    Transform target;
    NavMeshAgent agent;
    
    GameObject weapon;
    BoxCollider colliderWeapon;

    void Start() {
        anim = GetComponentInChildren<Animator>();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        weapon = GameObject.Find("Horn");
        colliderWeapon = weapon.GetComponent<BoxCollider>();
        colliderWeapon.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        float distance = Vector3.Distance(target.position, transform.position);
        anim.SetBool("Walk Forward", false);

        // Conditional statement checks if the distance between the enemy and player is less than or equal to the enemy's look radius
        if(distance <= lookRadius) {
            agent.SetDestination(target.position);
            anim.SetBool("Walk Forward", true);

            if(distance <= agent.stoppingDistance) {
                anim.SetBool("Walk Forward", false);

                // Attack the target
                if(CanAttack)
                    Attack();

                // Face the target
                FaceTarget();
            }
        }
    }

    public void Attack() {
        isAttacking = true;
        CanAttack = false;
        anim.SetTrigger("Stab Attack");
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackBool() {
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }
    
    IEnumerator ResetAttackCooldown() {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

    public void EnableAttack() {
        colliderWeapon.enabled = true;
    }

    public void DisableAttack() {
        colliderWeapon.enabled = false;
    }

    void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Display in editor
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}