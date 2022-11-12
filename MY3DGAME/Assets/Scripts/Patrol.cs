using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    //create a list of waypoints to travel
    public Transform[] waypoints;
    //create waypoint index for loop calculation
    private int _currentWaypointIndex;
    //set speed for enemy
    private float _speed = 2f;

    //set wait time for waypoint
    private float _waitTime = 1f;//in seconds
    private float _waitCounter = 0f;
    private bool _waiting = false;

    private void Update(){

        if(_waiting){
            _waitCounter += Time.deltaTime;
            if(_waitCounter < _waitTime){
                return;
            }
            _waiting = false;
        }

        Transform wp = waypoints[_currentWaypointIndex];
        //check if enemy reach a waypoint or not
        if(Vector3.Distance(transform.position, wp.position) < 0.01f){
            transform.position = wp.position;
            _waitCounter = 0f;
            _waiting = true;
            
            _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
        //if not keep move toward destination

        } else{
            transform.position = Vector3.MoveTowards(transform.position, wp.position, _speed * Time.deltaTime);
            transform.LookAt(wp.position);
        }
    }

}
