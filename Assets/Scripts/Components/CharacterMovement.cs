using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : CharacterAbilities {
    [SerializeField] private float walkSpeed = 5f;

    public float MoveSpeed { get; set; }

    protected override void Start() {
        base.Start();

        MoveSpeed = walkSpeed;
    }

    protected override void HandleAbility() {
        base.HandleAbility();

        MoveCharacter();
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
}
