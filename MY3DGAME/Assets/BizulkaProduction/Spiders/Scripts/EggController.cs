using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour{
    public float HatchTime;
    [SerializeField] GameObject _full;
    [SerializeField] GameObject _damaged;
    [SerializeField] private ParticleSystem _particleSystem;
    public GameObject Egg;
    public GameObject enemy;

    void Start()
    {
        StartHatchTime();
        enemy.gameObject.SetActive(false);
    }

    public void StartHatchTime()
    {
        StartCoroutine(Hatch());
    }

    IEnumerator Hatch(){
        yield return new WaitForSeconds(HatchTime);
        _full.gameObject.SetActive(false);
        _damaged.gameObject.SetActive(true);
        _particleSystem.Play();
        Destroy(Egg, 1.0f);
       enemy.gameObject.SetActive(true);
    }

}