using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : MonoBehaviour
{
    private Animator anim;

    public float maxHealth = 250f;
    public float currentHealth;

    public GameObject enemy;
    public HealthBar healthBar;
    public BossController movement;
    public Collider bossCollider;
    public GameObject rageSteam;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        rageSteam.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= maxHealth/2)
        {
            rageSteam.SetActive(true);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        movement.enabled = false;
        bossCollider.enabled = false;
        rageSteam.SetActive(false);
        anim.SetTrigger("Die");
        Destroy(enemy, 1.2f);
    }
}