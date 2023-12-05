using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Wander", fileName = "WanderAction")]
public class WanderAction : AIAction
{
    [Header("Wander Settings")]
    public float wanderArea = 3f; // tiles?
    public float wanderTime = 2f; // seconds

    [Header("Obstacle Settings")]
    public Vector2 obstacleBoxCheckSize = new Vector2(2, 2);
    public LayerMask obstacleMask;
    private Vector2 wanderDirection;
    private float wanderCheckTime;

    public override void Act(StateController controller)
    {
        EvaluateObstacles(controller);
        Wander(controller);
    }
    private void Wander(StateController controller)
    {
        if (Time.time > wanderCheckTime)
        {
            //Debug.Log("picking new position: " + wanderDirection.x + " " + wanderDirection.y);
            // pick random position and move to it
            wanderDirection.x = Random.Range(-wanderArea, wanderArea);
            wanderDirection.y = Random.Range(-wanderArea, wanderArea);

            controller.CharacterMovement.SetHorizontal(wanderDirection.x);
            controller.CharacterMovement.SetVertical(wanderDirection.y);

            // update time for next wander
            wanderCheckTime = Time.time + wanderTime;
        }
    }

    private void EvaluateObstacles(StateController controller)
    {
        RaycastHit2D hit = Physics2D.BoxCast(controller.Collider2D.bounds.center,
                                             obstacleBoxCheckSize,
                                             0f,
                                             wanderDirection,
                                             wanderDirection.magnitude,
                                             obstacleMask);

        if (hit)
        {
            wanderCheckTime = Time.time - 1; // force new wander call
        }
    }

    private void OnEnable()
    {
        wanderCheckTime = 0;
    }
}
