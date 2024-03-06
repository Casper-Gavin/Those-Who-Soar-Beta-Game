using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StateController : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private AIState currentState;
    [SerializeField] private AIState remainState;
    
    [Header("Field of View")]
    [SerializeField] private Light2D fieldOfView;

    public CharacterMovement CharacterMovement { get; set; }
    public Transform Target { get; set; }
    public Path Path { get; set; }
    public Light2D FieldOfView => fieldOfView;
    public Transform Player { get; set; }

    public PlayerHealth PlayerHealth {get; set;}

    public Collider2D Collider2D { get; set; }

    public BossCirclePattern BossCirclePattern {get; set;}
    public BossRandomPattern BossRandomPattern {get; set;}
    public BossSpiralPattern BossSpiralPattern {get; set;}

    public CharacterWeapon CharacterWeapon { get; set; }
    public CharacterFlip CharacterFlip { get; set; }

    private void Awake()
    {
        CharacterMovement = GetComponent<CharacterMovement>();
        CharacterWeapon = GetComponent<CharacterWeapon>();
        CharacterFlip = GetComponent<CharacterFlip>();
        Path = GetComponent<Path>();
        Collider2D = GetComponent<Collider2D>();

        Player = GameObject.FindWithTag("Player")?.transform;
        PlayerHealth = Player?.GetComponent<PlayerHealth>();
        currentState = currentState.DeepCopy(); // make a copy
        currentState.InitActions(this);

        BossCirclePattern = GetComponent<BossCirclePattern>();
        BossRandomPattern = GetComponent<BossRandomPattern>();
        BossSpiralPattern = GetComponent<BossSpiralPattern>();

    }

    private void Update()
    {
        currentState.EvaluateState(this);
    }

    public void TransitionToState(AIState nextState)
    {
        if (nextState != remainState)
        {
            currentState.DeepDelete(); // clean up old SO
            currentState = nextState.DeepCopy(); // make copy and reassign
            currentState.InitActions(this);
        }
    }

    // don't need - left in comments just in case
    // private void OnDrawGizmos()
    // {
    //     if (Player != null) // get rid of error, there is some time before the controller awakes where this throws errors
    //     {
    //         Gizmos.color = Color.red;
    //         Gizmos.DrawLine(transform.position, Player.position);
    //     }
    // }
}
