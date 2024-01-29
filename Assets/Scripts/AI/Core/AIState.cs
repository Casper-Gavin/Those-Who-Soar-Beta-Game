using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/State")]
public class AIState : ScriptableObject
{
    public AIAction[] Actions;
    public AITransition[] Transitions;

    public void InitActions(StateController controller)
    {
        foreach (AIAction action in Actions)
        {
            action.Init(controller);
        }
    }

    public void EvaluateState(StateController controller)
    {
        DoActions(controller);
        EvaluateTransitions(controller);
    }

    public void DoActions(StateController controller)
    {
        foreach (AIAction action in Actions)
        {
            action.Act(controller);
        }
    }

    public void EvaluateTransitions(StateController controller)
    {
        foreach (AITransition transition in Transitions)
        {
            bool result = transition.Decision.Decide(controller);
            controller.TransitionToState(result ? transition.TrueState : transition.FalseState);
        }
    }
}
