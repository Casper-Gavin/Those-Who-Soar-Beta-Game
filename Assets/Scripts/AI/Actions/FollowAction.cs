using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Follow", fileName = "FollowAction")]
public class FollowAction : AIAction
{
    public float minDistanceToFollow = 1f;

    public override void Init(StateController controller)
    {
        // do nothing
    }

    public override void Act(StateController controller)
    {
        FollowTarget(controller);
    }

    private void FollowTarget(StateController controller)
    {
        if (controller.Target == null)
        {
            return;
        }

        // horizontal
        if (controller.transform.position.x < controller.Target.position.x)
        {
            controller.CharacterMovement.SetHorizontal(1);
        }
        else
        {
            controller.CharacterMovement.SetHorizontal(-1);
        }

        if (Mathf.Abs(controller.transform.position.x - controller.Target.position.x) < minDistanceToFollow)
        {
            controller.CharacterMovement.SetHorizontal(0);
                       
        }

        // vertical
        if (controller.transform.position.y < controller.Target.position.y)
        {
            controller.CharacterMovement.SetVertical(1);
        }
        else
        {
            controller.CharacterMovement.SetVertical(-1);
        }

        if (Mathf.Abs(controller.transform.position.y - controller.Target.position.y) < minDistanceToFollow)
        {     
            controller.CharacterMovement.SetVertical(0); 
        }
    }
}
