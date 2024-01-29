using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Melee", fileName = "MeleeAction")]
public class MeleeAction : AIAction
{
    public override void Init(StateController controller)
    {
        // do nothing
    }

    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {
        controller.CharacterMovement.SetHorizontal(0f);
        controller.CharacterMovement.SetVertical(0f);

        controller.CharacterWeapon.CurrentWeapon.Attack();
    }
}
