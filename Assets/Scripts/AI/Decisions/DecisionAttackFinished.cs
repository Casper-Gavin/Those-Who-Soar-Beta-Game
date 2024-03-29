using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/AttackFinished", fileName = "AttackFinished")]
public class DecisionAttackFinished : AIDecision
{

    public override void Init(StateController controller)
    {
        // nothing
    }
    public override bool Decide(StateController controller)
    {
        return AttackCompleted(controller);
    }

    private bool AttackCompleted(StateController controller)
    {
        if (controller.CharacterWeapon.CurrentWeapon.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length
        > controller.CharacterWeapon.CurrentWeapon.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime)
        {
            return true;
        }

        return false;
    }
}
