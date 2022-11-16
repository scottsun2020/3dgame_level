using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LongRangeEnemyStats : MonoBehaviour {
    private Animator anim;

    public float maxHealth = 100f;
    public float currentHealth;

    public GameObject enemy;
    public HealthBar healthBar;
    public LongRangeEnemyController movement;
    public Collider enemyCollider;

    public PlayerMovement player;
    public TextMeshProUGUI enemyCountText;
    public GameObject victoryTextObject;

    void Start() {
        anim = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        SetCountText(player.enemiesDefeated);
        victoryTextObject.SetActive(false);
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
        player.enemiesDefeated++;
        SetCountText(player.enemiesDefeated);
    }

    void SetCountText(int count) {
        enemyCountText.text = "Enemies Defeated: " + count.ToString();
        if(count >= 2) {
            victoryTextObject.SetActive(true);
        }
    }
}