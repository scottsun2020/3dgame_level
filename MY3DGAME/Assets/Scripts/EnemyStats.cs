using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {
    private Animator anim;

    public float maxHealth = 100f;
    public float currentHealth;

    public GameObject enemy;
    public HealthBar healthBar;
    public EnemyController movement;
    public Collider enemyCollider;

    void Start() {
        anim = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0) {
            Die();
        }
    }

    public void Die() {
        movement.enabled = false;
        enemyCollider.enabled = false;
        anim.SetTrigger("Die");
        Destroy(enemy, 1.2f);
    }
}