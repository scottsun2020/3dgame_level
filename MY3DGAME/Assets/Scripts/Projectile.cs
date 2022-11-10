using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {
    // this is the projectile's speed
    public float m_Speed = 10f;
    
    // this is the projectile's lifespan (in seconds)
    public float m_Lifespan = 3f;

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
            Debug.Log("Player hit " + other.name + "!");
            other.GetComponent<Animator>().SetTrigger("Take Damage");
            GameObject a = Instantiate(HitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
            Destroy(a, 0.5f);
        }
    }
}