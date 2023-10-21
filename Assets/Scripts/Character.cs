using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public enum CharacterType {
        Player,
        Enemy
    }
    // [SerializeField] allows private variables to be visible in the inspector
    [SerializeField] private CharacterType characterType;
    

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
