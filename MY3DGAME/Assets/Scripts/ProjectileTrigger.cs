using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrigger : MonoBehaviour {
    // this is a reference to your projectile prefab
    public GameObject m_Projectile;
    
    // this is a reference to the transform where the prefab will spawn
    public Transform m_SpawnTransform;

    private void Update() {
        if (Input.GetMouseButtonDown(1)) {
            GameObject projectile = Instantiate(m_Projectile, m_SpawnTransform.position, m_SpawnTransform.rotation);
        }
    }
}