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
    public GameObject victoryText;

    public GameObject HitParticle;
    public GameObject DeathParticle;

    void Start() {
        anim = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        SetCountText(player.enemiesDefeated);
        victoryText.SetActive(false);
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0) {
            Die();
        }
    }

    public void Die() {
        enemyCollider.enabled = false;
        movement.enabled = false;
        player.enemiesDefeated++;
        SetCountText(player.enemiesDefeated);

        anim.SetTrigger("Die");
        FindObjectOfType<AudioManager>().Play("Long Range Enemy Death");
        Invoke("DeathEffect", 1.6f);
    }

    void DeathEffect() {
        GameObject a = Instantiate(DeathParticle, new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z), enemy.transform.rotation);
        Destroy(a, 0.6f);
        Destroy(enemy);
    }

    void SetCountText(int count) {
        enemyCountText.text = "Enemies Defeated: " + count.ToString() + "/13";
        if(count >= 13) {
            enemyCountText.text = "Enemies Defeated: " + count.ToString() + "/13\nGo to research base (mountain) and defeat the boss!";
        }
    }

    void OnParticleCollision(GameObject other) {
        anim.SetTrigger("Take Damage");
        TakeDamage(5);

        GameObject a = Instantiate(HitParticle, new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z), enemy.transform.rotation);
        Destroy(a, 0.6f);
    }
}