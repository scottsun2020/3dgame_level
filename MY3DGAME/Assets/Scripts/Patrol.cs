using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {
    // Create a list of waypoints to travel
    public Transform[] waypoints;
    
    // Create waypoint index for loop calculation
    private int _currentWaypointIndex;

    // Set speed for enemy
    private float _speed = 2f;

    // Set wait time for waypoint in seconds
    private float _waitTime = 1f; 
    private float _waitCounter = 0f;
    private bool _waiting = false;

    private void Update() {
        if(_waiting) {
            _waitCounter += Time.deltaTime;
            if(_waitCounter < _waitTime) {
                return;
            }
            _waiting = false;
        }

        Transform wp = waypoints[_currentWaypointIndex];

        // Check if enemy has reached a waypoint or not
        if(Vector3.Distance(transform.position, wp.position) < 0.01f) {
            transform.position = wp.position;
            _waitCounter = 0f;
            _waiting = true;
            
            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
        }
        // If not keep move toward destination
        else {
            transform.position = Vector3.MoveTowards(transform.position, wp.position, _speed * Time.deltaTime);
            transform.LookAt(wp.position);
        }
    }
}