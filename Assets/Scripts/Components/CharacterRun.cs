using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRun : CharacterAbilities {
    [SerializeField] private float runSpeed = 10f;

    protected override void HandleInput() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            Run();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            StopRunning();
        }
    }

    private void Run() {
        movement.MoveSpeed = runSpeed;
    }

    private void StopRunning() {
        movement.ResetSpeed();
    }
}
