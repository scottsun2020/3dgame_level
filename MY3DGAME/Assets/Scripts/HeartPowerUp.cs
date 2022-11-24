using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPowerUp : MonoBehaviour {
    public float multiplier = 0.3f;
    // public float duration = 4f;

    public GameObject pickupEffect;

    private int heal = 20;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            // StartCoroutine(Pickup(other));
            Pickup(other);
        }
    }

    void Pickup(Collider player){
        // Debug.Log("Power Up picked up!");

        // Spawn a cool effect
        Instantiate(pickupEffect, transform.position, transform.rotation);
        
        // Make player's health increase
        PlayerStats stats = player.GetComponent<PlayerStats>();

        float boost = heal;
        // stats.currentHealth + heal;

        // Debug.Log("currentHealth: " + stats.currentHealth);
        // Debug.Log("multiplier: " + multiplier);
        // Debug.Log("boost" + boost);
        
        stats.HealthBoost(boost);

        //make power up invisible
        //GetComponent<MeshRenderer>().enabled = false;
        //GetComponent<Collider>().enabled = false;
        //wait x amount of seconds to back
        //yield return new WaitForSeconds(duration);
        //reverse the effect on our player
        //stats.health /= multiplier;
        //remove power up object

        Destroy(gameObject);
    }
}