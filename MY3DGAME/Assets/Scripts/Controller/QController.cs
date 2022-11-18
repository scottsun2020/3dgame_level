using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QController : MonoBehaviour
{
    public GameObject projectile;
    public PlayerMovement abilityController;
    public GameObject spawnPoint;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (abilityController.CanAttack && abilityController.isCooldown1 == false)
            {
                abilityController.AttackAbility1();
                StartCoroutine(Fireprojectile());

            }
            else
            {
                Debug.Log("Ability is in cooldown");
            }
        }
    }

    IEnumerator Fireprojectile()
    {
        yield return new WaitForSeconds(1.4f);
        GameObject a = Instantiate(projectile, new Vector3(spawnPoint.transform.position.x, transform.position.y, spawnPoint.transform.position.z + 0.7f), spawnPoint.transform.rotation);
        Destroy(a, 2.8f);
    }

}
