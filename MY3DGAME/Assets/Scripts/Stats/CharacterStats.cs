
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth {get; private set;}

    public Stat damage;
    public Stat armor;

    void Awake(){
        currentHealth = maxHealth;
    }



    public void TakeDamage(int damage){
        //damage will not below 0
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " Take " + damage + " damage.");

        if(currentHealth <= 0){
            Die();
        }
    }

    public virtual void Die(){
        //Die in some way
        //this method is meant to override

        Debug.Log(transform.name + " died.");
    }

}
