using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskGoToTarget : Node
{
    private Transform _transform;
    private Animator _animator;

    public TaskGoToTarget(Transform transform){
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate(){
        Transform target = (Transform) GetData("target");

        if(Vector3.Distance(_transform.position, target.position) > 0.01f){
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, GuardBT.speed *Time.deltaTime);
            _transform.LookAt(target.position);
            _animator.SetBool("Walk Forward", true);

        }

        state = NodeState.RUNNING;
        return state;
    }
}
