using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Can See Player", fileName = "DecisionCanSeePlayer")]
public class DecisionCanSeePlayer : AIDecision
{
    public LayerMask obstacleMask;
    private float viewDistance;
    private float viewAngle;

    public override bool Decide(StateController controller)
    {
        return CanSeePlayer(controller);
    }

    private bool CanSeePlayer(StateController controller)
    {
        if (controller.FieldOfView != null)
        {
            viewDistance = controller.FieldOfView.pointLightInnerRadius;

            viewAngle = controller.FieldOfView.pointLightInnerAngle;
        }

        float distanceToPlayer = (controller.Player.position - controller.transform.position).sqrMagnitude;
        if (distanceToPlayer < Mathf.Pow(viewDistance, 2))
        {
            Vector2 directionToPlayer = (controller.Player.position - controller.transform.position).normalized;
            Vector2 faceDirection = controller.CharacterFlip.FacingRight ? Vector2.right : Vector2.left;

            float middleAngle = Vector2.Angle(faceDirection, directionToPlayer);
            if (middleAngle < viewAngle / 2f)
            {
                if (Physics2D.Linecast(controller.transform.position, controller.Player.position, obstacleMask))
                {
                    return false;
                }
                controller.Target = controller.Player;
                return true;
            }
        }

        controller.Target = null;
        return false;
    }
}
