using System.Collections.Generic;
using BehaviorTree;

public class GuardBT : Tree {
    public UnityEngine.Transform[] waypoints;

    public static float speed = 2f;
    public static float fovRange = 6f;
    public static float attackRange = 1f;

    protected override Node SetupTree(){
        //order is important to list the order of tasks: check range, Approach Enemy, patrol
        Node root = new Selector(new List<Node>{

            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform),
                new TaskAttack(transform),
            }),
            new Sequence(new List<Node>{
                new CheckEnemyInFOVRange(transform),
                new TaskGoToTarget(transform),
            }),
            new TaskPatrol(transform, waypoints),
        });

        return root;
    }

    public void EnableAttack() {

    }

    public void DisableAttack() {

    }
}
