using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/RangeToAttack", fileName = "RangeToAttack")]
public class DecisionRangeToAttack : AIDecision
{
    public float minDistanceToAttack = 1.5f;

    public override bool Decide(StateController controller)
    {
        return PlayerInRangeToAttack(controller);
    }

    private bool PlayerInRangeToAttack(StateController controller)
    {
        if (controller.Target != null)
        {
            float dist = (controller.Target.position - controller.transform.position).sqrMagnitude;
            return dist < minDistanceToAttack;
        }
        return false;
    }
}
