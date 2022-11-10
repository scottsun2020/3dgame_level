using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public float maxHealth = 100f;
    public float currentHealth;

    public HealthBar healthBar;

    void Start() {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.K)) {
            TakeDamage(20);
        }
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    public void HealthBoost(float boost) {
        currentHealth += boost;

        if(currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }

        healthBar.SetHealth(currentHealth);
    }
}