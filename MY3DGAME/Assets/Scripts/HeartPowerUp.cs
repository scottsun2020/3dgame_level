using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPowerUp : MonoBehaviour
{
    public float multiplier = 0.3f;
   // public float duration = 4f;

    public GameObject pickupEffect;

    //public int boost = 20;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other){

        if(other.CompareTag("Player")){
            //StartCoroutin(Pickup(other));
            Pickup(other);
        }
    }

    void Pickup(Collider player){
        //spawn a cool effect
        //Debug.Log("Power Up picked up!");
        Instantiate(pickupEffect, transform.position, transform.rotation);

        //int boost = 20;
        //make player's health increase
        PlayerStats stats = player.GetComponent<PlayerStats>();
        Debug.Log("currentHealth: " + stats.currentHealth);
        Debug.Log("multiplier: " + multiplier);

        float boost = stats.currentHealth * multiplier;
        Debug.Log("boost" + boost);
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