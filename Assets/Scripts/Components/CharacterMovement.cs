using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static SkillMenu;

public class CharacterMovement : CharacterAbilities {
    [SerializeField] private float walkSpeed = 5f;

    private SkillMenu skillMenu;
    private bool isSkilMenuMoveUnlockedOnce;

    public float MoveSpeed { get; set; }

    private readonly int isMovingParam = Animator.StringToHash("IsMoving");

    [SerializeField] private SoundManager soundManager;
    [SerializeField] private CharacterDash characterDash;

    private void Awake() {
        skillMenu = FindObjectOfType<SkillMenu>();

        soundManager = FindObjectOfType<SoundManager>();

        characterDash = GetComponent<CharacterDash>();
    }

    protected override void Start() {
        base.Start();

        MoveSpeed = walkSpeed;
    }

    protected override void Update() {
        base.Update();

        //skillMenu = FindObjectOfType<SkillMenu>();

        if (!isSkilMenuMoveUnlockedOnce && skillMenu.skillLevels[(int)SkillMenu.SkillEnum.IncreaseSpeed] > 0) {
            isSkilMenuMoveUnlockedOnce = true;
            walkSpeed = 7.5f;
        }

        if (soundManager == null) {
            soundManager = FindObjectOfType<SoundManager>();
        }

        if (characterDash == null) {
            characterDash = GetComponent<CharacterDash>();
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

        if (movementSpeed != Vector2.zero) {
            isCharcterMoving = true;

            if (character.CharacterTypes == Character.CharacterTypeEnum.Player) {
                if (!characterDash.isDashing) {
                    if (MoveSpeed == walkSpeed) {
                        soundManager.WalkingSound();
                    } else {
                        soundManager.RunningSound();
                    }
                }
            }
        } else {
            isCharcterMoving = false;

            soundManager.audioSource.Stop();
        }

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
