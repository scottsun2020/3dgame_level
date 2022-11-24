using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPowerUp : MonoBehaviour {
    public float speedUpEffect = 3.0f;
    public PlayerMovement movement;
    public GameObject pickupEffect;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player")){
            Pickup(other);
        }
    }

    void Pickup(Collider player){
        //spawn a cool effect
        //Debug.Log("Power Up picked up!");
        Instantiate(pickupEffect, transform.position, transform.rotation);

        //make player bigger
        //player.transform.localScale *= multiplier;
        //call the function of speed
        movement.SpeedUp(speedUpEffect);
        //reverse the effect on our player

        //remove power up object
        Destroy(gameObject);
    }
}