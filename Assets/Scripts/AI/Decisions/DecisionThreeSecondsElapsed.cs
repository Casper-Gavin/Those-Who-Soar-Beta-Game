using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/ThreeSecondsElapsed", fileName = "ThreeSecondsElapsed")]
public class DecisionThreeSecondsElapsed : AIDecision
{
    private float timePassed = 0.0f;
    public override void Init(StateController controller)
    {
        timePassed = 0.0f;
    }
    public override bool Decide(StateController controller)
    {
        return ThreeSecondsElapsed(controller);
    }

    private bool ThreeSecondsElapsed(StateController controller)
    {
        timePassed += Time.deltaTime;
        if (timePassed > 3.0f)
        {
            return true;
        }

        return false;
    }
}
