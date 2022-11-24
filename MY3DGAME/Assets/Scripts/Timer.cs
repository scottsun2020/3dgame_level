using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {
    private EggController eggController;
    public BossController controller;
    
    // Start time value
    [SerializeField] float startTime;

    // Current Time
    float currentTime;

    // Whether the timer started?
    bool timerStarted = false;

    // Ref var for my TMP text component
    [SerializeField] TMP_Text timerText;

    // Start is called before the first frame update
    void Start() {
        controller.enabled = false;
        eggController = GetComponent<EggController>();
        
        //resets the currentTime to the start Time 
        currentTime = startTime;

        //displays the UI with the currentTime
        timerText.text = currentTime.ToString();
        
        // starts the time -- comment this out if you don't want to automatically start
        timerStarted = true;
    }

    // Update is called once per frame
    void Update() {
        if (timerStarted) {
            // Subtracting the previous frame's duration
            currentTime -= Time.deltaTime;

            // Logic current reached 0?
            if (currentTime <= 0) {
                eggController.Destroy();
                controller.enabled = true;
                // Debug.Log("timer reached zero");
                timerStarted = false;
                currentTime = 0;
            }

            timerText.text = "Time " + currentTime.ToString("f1");
        }
    }
}