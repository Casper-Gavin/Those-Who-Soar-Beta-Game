using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilities : MonoBehaviour {
    protected float horizantalInput;
    protected float verticalInput;

    protected PlayerController controller;
    protected CharacterMovement movement;
    protected CharacterWeapon characterWeapon;
    protected Animator animator;
    protected Character character;

    // Start is called before the first frame update
    protected virtual void Start() {
        controller = GetComponent<PlayerController>();
        character = GetComponent<Character>();
        characterWeapon = GetComponent<CharacterWeapon>();
        movement = GetComponent<CharacterMovement>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update() {
        HandleAbility();
        HandleInput();
    }

    /// <summary>
    /// Here, we put the logic of each ability
    /// 
    /// We use protected virtual so that we can override this
    /// function in our child classes
    /// </summary>
    protected virtual void HandleAbility() {
        InternalUpdate();
    }

    /// <summary>
    /// Here, we get the necessary input for each ability
    /// and call the function(s) or action(s) associated with it
    /// </summary>
    protected virtual void HandleInput() {
        
    }

    /// <summary>
    /// Here, we get the main input needed for controlling
    /// our character
    /// </summary>
    protected virtual void InternalUpdate() {
        horizantalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
}
