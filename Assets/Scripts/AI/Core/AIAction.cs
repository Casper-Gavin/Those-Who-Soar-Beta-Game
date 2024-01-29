using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : ScriptableObject
{
    public abstract void Init(StateController controller);
    public abstract void Act(StateController controller);
}
