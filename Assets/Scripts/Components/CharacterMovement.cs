using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : CharacterAbilities {
    [SerializeField] private float walkSpeed = 5f;

    public float WalkMoveSpeed { get; set; }

    protected override void Start() {
        base.Start();

        WalkMoveSpeed = walkSpeed;
    }

    protected override void HandleAbility() {
        base.HandleAbility();

        MoveCharacter();
    }

    private void MoveCharacter() {
        Vector2 movement = new Vector2(horizantalInput, verticalInput);
        Vector2 movementNormalized = movement.normalized;
        Vector2 movementSpeed = movementNormalized * WalkMoveSpeed;

        controller.SetMovement(movementSpeed);
    }
}
