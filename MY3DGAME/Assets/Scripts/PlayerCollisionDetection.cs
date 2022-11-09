using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour {
    public EnemyController weaponController;
    public GameObject HitParticle;

    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" && weaponController.isAttacking) {
            // Debug.Log("Enemy hit " + other.name + "!");

            PlayerStats stats = other.GetComponent<PlayerStats>();
            stats.TakeDamage(10);

            GameObject a = Instantiate(HitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
            Destroy(a, 0.5f);
        }
    }
}