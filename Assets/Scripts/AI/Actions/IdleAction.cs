using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Idle", fileName = "IdleAction")]
public class IdleAction : AIAction
{
    public override void Init(StateController controller)
    {
        // do nothing
    }

    public override void Act(StateController controller)
    {
        controller.CharacterMovement.SetHorizontal(0);
        controller.CharacterMovement.SetVertical(0);
    }
}
