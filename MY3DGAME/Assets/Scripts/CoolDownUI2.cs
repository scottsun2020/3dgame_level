using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownUI2 : MonoBehaviour
{
    public Image imageCooldown;
    [SerializeField] private float cooldown;
    bool isCooldown;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isCooldown = true;
        }

        if (isCooldown)
        {
            imageCooldown.fillAmount += 1 / cooldown * Time.deltaTime;

            if (imageCooldown.fillAmount >= 1)
            {
                imageCooldown.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
}
