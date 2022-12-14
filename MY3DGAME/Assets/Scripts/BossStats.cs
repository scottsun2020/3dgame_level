using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : MonoBehaviour {
    private Animator anim;

    public float maxHealth = 250f;
    public float currentHealth;

    public GameObject enemy;
    public HealthBar healthBar;
    public BossController movement;
    public Collider bossCollider;
    public GameObject rageSteam;

    public GameObject HitParticle;
    public GameObject victoryText;


    void Start() {
        anim = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rageSteam.SetActive(false);
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= maxHealth/2) {
            rageSteam.SetActive(true);
        }

        if (currentHealth <= 0) {
            Die();
        }
    }

    public void Die() {
        movement.enabled = false;
        bossCollider.enabled = false;
        rageSteam.SetActive(false);
        anim.SetTrigger("Die");
        FindObjectOfType<AudioManager>().Play("Enemy Death");
        victoryText.SetActive(true);
        Destroy(enemy, 1.5f);
    }

    void OnParticleCollision(GameObject other) {
        anim.SetTrigger("Take Damage");
        Instantiate(HitParticle, new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z), enemy.transform.rotation);
        TakeDamage(5);
    }
}