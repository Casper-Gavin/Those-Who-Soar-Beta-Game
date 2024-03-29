using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/TeleportFinished", fileName = "TeleportFinished")]
public class DecisionTeleportFinished : AIDecision
{
    public override bool Decide(StateController controller)
    {
        return TeleportFinished(controller);
    }

    private bool TeleportFinished(StateController controller)
    {
        if (controller.transform.GetChild(1).GetComponent<SpriteRenderer>().color.a == 1.0f) // if this doesnt work, add flag to teleporter component
        {
            return true;
        }
        return false;
    }
}
