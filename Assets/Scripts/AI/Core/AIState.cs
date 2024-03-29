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

    public void InitTransitions(StateController controller)
    {
        foreach (AITransition transition in Transitions)
        {
            transition.Decision.Init(controller);    
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

    // Could be memory leaking - hopefully not - oh well c# should clean it up
    public AIState DeepCopy()
    {
        AIState deepCopy = Instantiate(this);
        deepCopy.Actions = new AIAction[Actions.Length];
        for (int i = 0; i < deepCopy.Actions.Length; i++)
        {
            deepCopy.Actions[i] = Instantiate(Actions[i]);            
        }
        return deepCopy;
    }

    public void DeepDelete()
    {
        for (int i = 0; i < Actions.Length; i++)
        {
            Destroy(Actions[i]);            
        }
        Destroy(this);
    }
}
