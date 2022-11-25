using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckEnemyInFOVRange : Node {
    private static int _enemyLayerMask = 1 << 6;
    private Transform _transform;
    private Animator _animator;

    public CheckEnemyInFOVRange(Transform transform) {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate() {
        object t = GetData("target");
        if(t == null) {
            Collider[] colliders = Physics.OverlapSphere(_transform.position, GuardBT.fovRange, _enemyLayerMask);
            //Debug.Log("my enemy : " + colliders[0].transform);


            if(colliders.Length > 0) {
                parent.parent.SetData("target", colliders[0].transform);
                // Debug.Log("my target: " + colliders[0].transform);
                // Debug.Log("my enemy layer: " + _enemyLayerMask);
                // Debug.Log("my Range : " + GuardBT.fovRange);
                // Debug.Log("my length : " + colliders.Length);

                _animator.SetBool("Walk Forward", true);
                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }
}