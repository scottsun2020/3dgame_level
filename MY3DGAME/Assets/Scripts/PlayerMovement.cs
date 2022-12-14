using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private CharacterController controller;
    private Animator anim;
    
    GameObject weapon;
    BoxCollider colliderWeapon;
    
    public GameObject playerProjectile;
    public Transform projectileSpawn;
    public int enemiesDefeated;

    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public bool isAttacking = false;
    public bool isCooldown1;
    public bool isCooldown2;

    private Vector3 moveDirection;
    private Vector3 velocity;
    
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float turnSmoothVelocity;
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpHeight;

    [SerializeField] private float Ability1Cooldowm;
    [SerializeField] private float Ability2Cooldowm;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    // Start is called before the first frame update
    private void Start() {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        
        weapon = GameObject.Find("Weapon");
        colliderWeapon = weapon.GetComponent<BoxCollider>();
        colliderWeapon.enabled = false;
        
        enemiesDefeated = 0;
    }

    // Update is called once per frame
    private void Update() {
        Move();
    }

    private void Move() {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if(isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        
        moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        moveDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * moveDirection;

        if(isGrounded) {
            if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift)) {
                // Play running animation and call Run() function
                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0, angle, 0);
                Run();
            }
            else if(moveDirection != Vector3.zero) {
                // Play walking animation and call Walk() function
                float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0, angle, 0);
                Walk();
            }
            else if(moveDirection == Vector3.zero) {
                // Play idle animation and call Idle() function
                Idle();
            }

            moveDirection *= movementSpeed;

            if(Input.GetMouseButtonDown(0)) {
                if(CanAttack) {
                    Attack();
                }
            }

            if(Input.GetKeyDown(KeyCode.E)) {
                if(CanAttack && isCooldown2 == false) {
                    AttackAbility2();
                }
                else {
                    // Debug.Log("Ability is in cooldown");
                }
            }

            if(Input.GetKeyDown(KeyCode.Space)) {
                Dodge();
            }
        }

        controller.Move(moveDirection * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Idle() {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk() {
        movementSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }
    
    public void Step() {
        FindObjectOfType<AudioManager>().Play("Footsteps");
    }

    private void Run() {
        movementSpeed = runSpeed;
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    public void EnableAttack() {
        colliderWeapon.enabled = true;
    }

    public void DisableAttack() {
        colliderWeapon.enabled = false;
    }

    public void EnableDodge() {
        controller.detectCollisions = false;
    }

    public void DisableDodge() {
        controller.detectCollisions = true;
    }

    public void Attack() {
        isAttacking = true;
        CanAttack = false;
        int attackIndex = Random.Range(1, 3);
        anim.SetTrigger("Attack" + attackIndex);
        FindObjectOfType<AudioManager>().Play("Swing");
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown() {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

    IEnumerator ResetAbility1Cooldown() {
        yield return new WaitForSeconds(Ability1Cooldowm);
        isCooldown1 = false;
    }

    IEnumerator ResetAbility2Cooldown() {
        yield return new WaitForSeconds(Ability2Cooldowm);
        isCooldown2 = false;
    }

    IEnumerator ResetAttackBool() {
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }

    public void AttackAbility1() {
        isAttacking = true;
        CanAttack = false;
        isCooldown1 = true;
        
        anim.SetTrigger("Ability 1");
        StartCoroutine(ResetAttackCooldown());
        StartCoroutine(ResetAbility1Cooldown());
    }

    public void AttackAbility2() {
        isAttacking = true;
        CanAttack = false;
        isCooldown2 = true;

        anim.SetTrigger("Casting");
        Invoke("CastProjectile", 0.28f);

        StartCoroutine(ResetAttackCooldown());
        StartCoroutine(ResetAbility2Cooldown());
    }

    void CastProjectile() {
        GameObject projectile = Instantiate(playerProjectile, projectileSpawn.position, projectileSpawn.rotation);
        FindObjectOfType<AudioManager>().Play("Cast Projectile");
    }

    private void Dodge() {
        isAttacking = true;
        CanAttack = false;
        
        anim.SetTrigger("Dodge");
        StartCoroutine(ResetAttackCooldown());
    }

    public void SlowDown(float effect) {
        StartCoroutine(SlowDownDuration());
        if (movementSpeed > 2.0f) {
            movementSpeed -= effect;
        }
        if (walkSpeed > 2.0f) {
            walkSpeed -= effect;
        }
        if (runSpeed > 2.0f) {
            runSpeed -= effect;
        }
        else
            Debug.Log("Cannot slow down anymore");
    }

    IEnumerator SlowDownDuration() {
        yield return new WaitForSeconds(2.0f);
        movementSpeed += 1.0f;
        walkSpeed += 1.0f;
        runSpeed += 1.0f;
    }

    public void SpeedUp(float effect) {
        StartCoroutine(SpeedUpDuration());
        if (movementSpeed < 12.0f) {
            movementSpeed += effect;
        }
        if (walkSpeed < 12.0f) {
            walkSpeed += effect;
        }
        if (runSpeed < 12.0f) {
            runSpeed += effect;
        }
        else {
            // Debug.Log("Cannot speed up anymore");
        }
    }

    IEnumerator SpeedUpDuration() {
        yield return new WaitForSeconds(10.0f);
        movementSpeed -= 3.0f;
        walkSpeed -= 3.0f;
        runSpeed -= 3.0f;
    }

    private void OnApplicationFocus(bool focus) {
        if(focus) {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}