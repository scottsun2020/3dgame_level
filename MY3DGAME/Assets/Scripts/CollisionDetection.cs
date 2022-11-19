using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour {
    public PlayerMovement weaponController;
    public GameObject HitParticle;

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy" && weaponController.isAttacking) {
            // Debug.Log("Player hit " + other.name + "!");
            other.GetComponent<Animator>().SetTrigger("Take Damage");

            EnemyStats stats = other.GetComponent<EnemyStats>();
            LongRangeEnemyStats stats2 = other.GetComponent<LongRangeEnemyStats>();
            BossStats stats3 = other.GetComponent<BossStats>();
            
            // This is for Behavior tree Enemy
            CharacterStats stats4 = other.GetComponent<CharacterStats>();

            if (stats != null) {
                stats.TakeDamage(25);
                GameObject a = Instantiate(HitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
                Destroy(a, 0.5f);
            }
            else if (stats2 != null) {
                stats2.TakeDamage(25);
                GameObject a = Instantiate(HitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
                Destroy(a, 0.5f);
            }
            else if (stats3 != null) {
                stats3.TakeDamage(25);
                GameObject a = Instantiate(HitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
                Destroy(a, 0.5f);
            }
            else if (stats4 != null) {
                stats4.TakeDamage(10);
                GameObject a = Instantiate(HitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
                Destroy(a, 0.5f);
            }
        }
    }
}