using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
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
        
    }

    // FixedUpdate is called once per physics update (0.02s)
    void FixedUpdate() {
        
    }
}
