using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskPatrol : Node
{
    private Transform _transform;
    private Animator _animator;
    private Transform[] _waypoints;

    private int _currentWaypointIndex = 0;

    //set wait time for waypoint
    private float _waitTime = 2f;//in seconds
    private float _waitCounter = 0f;
    private bool _waiting = false;

    public TaskPatrol(Transform transform, Transform[] waypoints){
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        _waypoints = waypoints;
    }

    public override NodeState Evaluate(){
        if(_waiting){
            _waitCounter += Time.deltaTime;
            if(_waitCounter > _waitTime){
                _waiting = false;
                //_animator.SetBool("Jump", false);
                _animator.SetBool("Walk Forward", true);
            }
            
        }else{
            Transform wp = _waypoints[_currentWaypointIndex];
            //check if enemy reach a waypoint or not
            if(Vector3.Distance(_transform.position, wp.position) < 0.01f){
                _transform.position = wp.position;
                _waitCounter = 0f;
                _waiting = true;
            
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                _animator.SetBool("Walk Forward", false);

                //if not keep move toward destination

            } else{
                _transform.position = Vector3.MoveTowards(_transform.position, wp.position, GuardBT.speed * Time.deltaTime);
                _transform.LookAt(wp.position);
            }

        }

        state = NodeState.RUNNING;
        return state;

        
    }
}
