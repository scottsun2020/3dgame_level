using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour {
    public PlayerMovement weaponController;
    public GameObject HitParticle;

    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Enemy" && weaponController.isAttacking) {
            Debug.Log("Player hit " + other.name + "!");
            other.GetComponent<Animator>().SetTrigger("Take Damage");
            GameObject a = Instantiate(HitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
            Destroy(a, 0.5f);
        }
    }
}