using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    [SerializeField] private FieldOfView fieldOfView;

    public enum CharacterTypeEnum {
        Player,
        Enemy
    }
    // check Player Damage damageType

    // [SerializeField] allows private variables to be visible in the inspector
    [SerializeField] private CharacterTypeEnum characterType;
    [SerializeField] private GameObject characterSprite;
    [SerializeField] private Animator characterAnimator;

    // switched from the tutorial - CharacterType vs CharacterTypes with above
    public CharacterTypeEnum CharacterTypes => characterType;
    public GameObject CharacterSprite => characterSprite;
    public Animator CharacterAnimator => characterAnimator;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        // fieldOfView.SetAimDirection(aimDir);
        fieldOfView.SetOrigin(transform.position);
    }

    // FixedUpdate is called once per physics update (0.02s)
    void FixedUpdate() {
        
    }
}
