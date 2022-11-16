using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour {
    private Animator anim;

    public float maxHealth = 100f;
    public float currentHealth;

    public HealthBar healthBar;
    public PlayerMovement movement;
    public Collider playerCollider;

    bool gameHasEnded = false;
    public float restartDelay = 5f;

    void Start() {
        anim = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        // Debug.Log(transform.name + " Take " + damage + " damage.");

        if(currentHealth <= 0) {
            Die();
        }
    }

    public bool TakeHit()
    {
        currentHealth-= 10;
        healthBar.SetHealth(currentHealth);

        bool isDead = currentHealth <= 0;
        if (isDead) Die();
        return isDead;
    }

    public void HealthBoost(float boost) {
        currentHealth += boost;

        if(currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }

        healthBar.SetHealth(currentHealth);
    }

    public void Die() {
        if(gameHasEnded == false) {
            gameHasEnded = true;
            movement.enabled = false;
            playerCollider.enabled = false;
            anim.SetTrigger("Death");
            Invoke("Restart", restartDelay);
        }
    }

    void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}