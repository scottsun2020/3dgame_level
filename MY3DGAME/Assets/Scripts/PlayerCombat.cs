using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {
    private Animator anim;

    // Data fields for combat system
    [SerializeField] public float cooldownTime = 2f;
    [SerializeField] private float nextFireTime = 0f;
    [SerializeField] public static int noOfClicks = 0;
    [SerializeField] float lastClickedTime = 0;
    [SerializeField] float maxComboDelay = 1;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if(noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1")) {
            anim.SetBool("Attack1", false);
        }
        if(noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2")) {
            anim.SetBool("Attack2", false);
        }
        if(noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1")) {
            anim.SetBool("Attack3", false);
            noOfClicks = 0;
        }

        if(Time.time - lastClickedTime > maxComboDelay) {
            noOfClicks = 0;
        }

        if(Time.time > nextFireTime) {
            if(Input.GetMouseButtonDown(0)) {
                onClick();
            }
        }
    }

    private void onClick() {
        lastClickedTime = Time.time;
        noOfClicks++;

        if(noOfClicks == 1) {
            anim.SetBool("Attack1", true);
        }

        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        if(noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1")) {
            anim.SetBool("Attack1", false);
            anim.SetBool("Attack2", true);
        }

        if(noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2")) {
            anim.SetBool("Attack2", false);
            anim.SetBool("Attack3", true);
        }
    }
}