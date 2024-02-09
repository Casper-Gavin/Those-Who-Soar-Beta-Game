using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : CharacterAbilities {
    [SerializeField] private float walkSpeed = 5f;

    private SkillMenu skillMenu;
    private bool isSkilMenuMoveUnlockedOnce;

    public float MoveSpeed { get; set; }

    private readonly int isMovingParam = Animator.StringToHash("IsMoving");

    private void Awake() {
        skillMenu = FindObjectOfType<SkillMenu>();
    }

    protected override void Start() {
        base.Start();

        MoveSpeed = walkSpeed;
    }

    protected override void Update() {
        base.Update();

        if (!isSkilMenuMoveUnlockedOnce && skillMenu.skillLevels[(int)SkillMenu.SkillEnum.IncreaseSpeed] > 0) {
            isSkilMenuMoveUnlockedOnce = true;
            walkSpeed = 7.5f;
        }
    }

    protected override void HandleAbility() {
        base.HandleAbility();

        MoveCharacter();

        UpdateAnimations();
    }

    private void MoveCharacter() {
        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        Vector2 moveInput = movement;
        Vector2 movementNormalized = moveInput.normalized;
        Vector2 movementSpeed = movementNormalized * MoveSpeed;

        controller.SetMovement(movementSpeed);
    }

    public void ResetSpeed () {
        MoveSpeed = walkSpeed;
    }

    private void UpdateAnimations() {
        if (character.CharacterTypes == Character.CharacterTypeEnum.Player)
        {
            if (horizontalInput > 0.1f || verticalInput > 0.1f || horizontalInput < -0.1f || verticalInput < -0.1f) {
                if (character.CharacterAnimator != null) {
                    character.CharacterAnimator.SetBool(isMovingParam, true);
                }
            } else {
                if (character.CharacterAnimator != null) {
                    character.CharacterAnimator.SetBool(isMovingParam, false);
                }
            }
        }
    }

    public void SetHorizontal(float value)
    {
        horizontalInput = value;
    }

    public void SetVertical(float value)
    {
        verticalInput = value;
    }
}
