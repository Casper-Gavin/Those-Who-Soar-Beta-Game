using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDash : CharacterAbilities {
    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashDuration = 0.5f;

    private bool isDashing;
    private float dashTimer;
    private Vector2 dashOrigin;
    private Vector2 dashDestination;
    private Vector2 newPlayerPosition;

    protected override void HandleInput() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Dash();
        }
    }

    protected override void HandleAbility() {
        base.HandleAbility();

        if (isDashing) {
            if (dashTimer < dashDuration) {
                newPlayerPosition = Vector2.Lerp(dashOrigin, dashDestination, dashTimer / dashDuration);
                controller.MovePosition(newPlayerPosition);
                dashTimer += Time.deltaTime;
            } else {
                StopDash();
            }
        }
    }

    private void Dash() {
        isDashing = true;
        dashTimer = 0f;
        dashOrigin = transform.position;

        controller.NormalMovement = false;

        dashDestination = transform.position + (Vector3)controller.CurrentMovement.normalized * dashDistance;
    }

    private void StopDash() {
        isDashing = false;
        controller.NormalMovement = true;
    }
}
