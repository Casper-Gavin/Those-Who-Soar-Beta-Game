using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/TeleportFinished", fileName = "TeleportFinished")]
public class DecisionTeleportFinished : AIDecision
{
    public override void Init(StateController controller)
    {
        // nothing
    }

    public override bool Decide(StateController controller)
    {
        return TeleportFinished(controller);
    }

    private bool TeleportFinished(StateController controller)
    {
        if (controller.transform.GetComponentInChildren<Teleporter>().finished) 
        {
            SpriteRenderer spriteRenderer = controller.transform.GetChild(0).GetComponent<SpriteRenderer>();
            Color c = spriteRenderer.color;
            c.a = 1.0f;
            spriteRenderer.color = c;
            controller.transform.GetComponentInChildren<Teleporter>().finished = false;
            return true;
        }
        return false;
    }
}
