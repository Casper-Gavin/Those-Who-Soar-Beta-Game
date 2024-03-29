using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : ScriptableObject
{
    public abstract void Init(StateController controller);
    public abstract bool Decide(StateController controller);
}
