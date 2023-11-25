using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Decision Test")]
public class DecisionTest : AIDecision
{
    public override bool Decide(StateController controller)
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            return true;
        }
        return false;
    }
}
