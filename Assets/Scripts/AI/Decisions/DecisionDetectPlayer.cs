using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Detect Player", fileName = "DecisionDetectPlayer")]
public class DecisionDetectPlayer : AIDecision
{
    public float detectArea = 3f;
    public LayerMask targetMask;

    private Collider2D targetCollider2D;

    public override bool Decide(StateController controller)
    {
        return CheckTarget(controller);   
    }

    private bool CheckTarget(StateController controller)
    {
        targetCollider2D = Physics2D.OverlapCircle(controller.transform.position, detectArea, targetMask);
        if (targetCollider2D != null) // found target
        {
            controller.Target = targetCollider2D.transform; // set target
            return true;
        }

        return false;
    }
}