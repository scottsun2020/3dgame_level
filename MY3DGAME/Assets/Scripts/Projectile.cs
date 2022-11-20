using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {
    // this is the projectile's speed
    public float m_Speed = 650f;
    
    // this is the projectile's lifespan (in seconds)
    public float m_Lifespan = 1.5f;

    private Rigidbody m_Rigidbody;

    public GameObject HitParticle;

    void Awake() {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Start() {
        m_Rigidbody.AddForce(m_Rigidbody.transform.forward * m_Speed);
        Destroy(gameObject, m_Lifespan);
    }

    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Enemy") {
            other.GetComponent<Animator>().SetTrigger("Take Damage");

            EnemyStats stats = other.GetComponent<EnemyStats>();
            LongRangeEnemyStats stats2 = other.GetComponent<LongRangeEnemyStats>();
            BossStats stats3 = other.GetComponent<BossStats>();
            
            // This is for Behavior tree Enemy
            CharacterStats stats4 = other.GetComponent<CharacterStats>();

            if (stats != null) {
                stats.TakeDamage(35);
                GameObject a = Instantiate(HitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
                Destroy(a, 0.5f);
            }
            else if (stats2 != null) {
                stats2.TakeDamage(35);
                GameObject a = Instantiate(HitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
                Destroy(a, 0.5f);
            }
            else if (stats3 != null) {
                stats3.TakeDamage(35);
                GameObject a = Instantiate(HitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
                Destroy(a, 0.5f);
            }
            else if (stats4 != null) {
                stats4.TakeDamage(35);
                GameObject a = Instantiate(HitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
                Destroy(a, 0.5f);
            }
        }
    }
}