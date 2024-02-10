using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SkillMenu;

public class CharacterRun : CharacterAbilities {
    [SerializeField] private float runSpeed = 10f;

    private SkillMenu skillMenu;
    private bool isSkilMenuRunUnlockedOnce;

    private void Awake() {
        skillMenu = FindObjectOfType<SkillMenu>();
    }

    protected override void Update() {
        base.Update();

        if (!isSkilMenuRunUnlockedOnce && skillMenu.skillLevels[(int)SkillMenu.SkillEnum.IncreaseSpeed] > 0) {
            isSkilMenuRunUnlockedOnce = true;
            runSpeed = 11f;
        }
    }

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
