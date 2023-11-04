using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : CharacterAbilities {
    [SerializeField] private float walkSpeed = 5f;

    public float MoveSpeed { get; set; }

    private readonly int isMovingParam = Animator.StringToHash("IsMoving");

    protected override void Start() {
        base.Start();

        MoveSpeed = walkSpeed;
    }

    protected override void HandleAbility() {
        base.HandleAbility();

        MoveCharacter();

        UpdateAnimations();
    }

    private void MoveCharacter() {
        Vector2 movement = new Vector2(horizantalInput, verticalInput);
        Vector2 movementNormalized = movement.normalized;
        Vector2 movementSpeed = movementNormalized * MoveSpeed;

        controller.SetMovement(movementSpeed);
    }

    public void ResetSpeed () {
        MoveSpeed = walkSpeed;
    }

    private void UpdateAnimations() {
        if (horizantalInput > 0.1f || verticalInput > 0.1f || horizantalInput < -0.1f || verticalInput < -0.1f) {
            character.CharacterAnimator.SetBool(isMovingParam, true);
        } else {
            character.CharacterAnimator.SetBool(isMovingParam, false);
        }
    }
}
