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
    public bool isCharcterMoving;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private CharacterDash characterDash;

    private void Awake() {
        skillMenu = FindObjectOfType<SkillMenu>();

        audioManager = FindObjectOfType<AudioManager>();

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

        if (audioManager == null) {
            audioManager = FindObjectOfType<AudioManager>();
        }

        if (characterDash == null) {
            characterDash = GetComponent<CharacterDash>();
        }

        if (horizontalInput != 0 || verticalInput != 0) {
            if (Input.GetKey(KeyCode.LeftShift)) {
                if (audioManager.GetCurrentlyPlayingSFX() == "Walk") {
                    audioManager.StopSFX("Walk");
                }
                
                if (!audioManager.IsPlayingSFX("Run")) {
                    audioManager.PlaySFX("Run");
                }
            } else {
                if (audioManager.GetCurrentlyPlayingSFX() == "Run") {
                    audioManager.StopSFX("Run");
                }

                if (!audioManager.IsPlayingSFX("Walk")) {
                    audioManager.PlaySFX("Walk");
                }    
            }
        } else {
            if (audioManager.GetCurrentlyPlayingSFX() == "Run") {
                audioManager.StopSFX("Run");
            }

            if (audioManager.GetCurrentlyPlayingSFX() == "Walk") {
                audioManager.StopSFX("Walk");
            }
        }

        //Debug.Log(audioManager.GetCurrentlyPlayingSFX());
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

        /*
        if (movementSpeed != Vector2.zero) {
            isCharcterMoving = true;
        } else {
            isCharcterMoving = false;
        }*/

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
