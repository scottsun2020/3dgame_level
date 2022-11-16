using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{
    private Animator anim;

    public float maxHealth = 50f;
    public float currentHealth;
    
    public HealthBar healthBar;
    public GameObject enemy;
    public Collider enemyCollider;


    //public Stat damage;

    void Start(){
        anim = GetComponentInChildren<Animator>();

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }



    public void TakeDamage(float damage){

        currentHealth -= damage;
        anim.SetTrigger("Take Damage");

        healthBar.SetHealth(currentHealth);

        //Debug.Log(transform.name + " Take " + damage + " damage.");

        if(currentHealth <= 0){
            Die();
        }
    }

    public void Die(){
        enemyCollider.enabled = false;
        anim.SetTrigger("Die");

        Destroy(enemy, 1f);
    }

}
