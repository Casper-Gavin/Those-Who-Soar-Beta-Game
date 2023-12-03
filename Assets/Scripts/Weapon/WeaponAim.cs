using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    [SerializeField] private GameObject reticlePrefab;

    public float CurrentAimAngleAbsolute { get; set; }
    public float CurrentAimAngle { get; set; }

    private Camera mainCamera;
    private GameObject reticle;
    private WeaponBase weapon;

    // all Vector3's will be used to calculate aim with each other

    private Vector3 direction;
    private Vector3 mousePosition;
    private Vector3 reticlePosition;
    private Vector3 currentAim = Vector3.zero;
    private Vector3 currentAimAbsolute = Vector3.zero;
    private Quaternion initialRotation;
    private Quaternion lookRotation;
    private Quaternion lastRotation;

    private void Start()
    {
        Cursor.visible = false;
        weapon = GetComponent<WeaponBase>();
        initialRotation = transform.rotation;

        mainCamera = Camera.main;
        reticle = Instantiate(reticlePrefab);
    }

    private void Update()
    {
        if (weapon.WeaponOwner.CharacterTypes == Character.CharacterType.Player)
        {
            GetMousePosition();
        }
        else
        {
            EnemyAim();
        }
        MoveReticle();
        RotateWeapon();
    }

    // locks mouse position in the z direction to make sure that the program can always see the mouse
    private void GetMousePosition()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = 5f;

        // needs position in world units
        direction = mainCamera.ScreenToWorldPoint(mousePosition);
        direction.z = transform.position.z;
        reticlePosition = direction;

        currentAimAbsolute = direction - transform.position;
        if ( weapon.WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
        {
            // if facing right
            currentAim = direction - transform.position;
        }
        else
        {
            currentAim = transform.position - direction;
        }
    }

    public void RotateWeapon()
    {
        if (currentAim != Vector3.zero && direction != Vector3.zero && UIManager.GameIsPaused == false)
        {
            // returns angle by passing in a direction
            CurrentAimAngle = Mathf.Atan2(currentAim.y, currentAim.x) * Mathf.Rad2Deg;
            CurrentAimAngleAbsolute = Mathf.Atan2(currentAimAbsolute.y, currentAimAbsolute.x) * Mathf.Rad2Deg;

            // clamps rotation of the reticle depending on the direction that that player is facing (180 degree rotation)
            if (weapon.WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
            {
                CurrentAimAngle = Mathf.Clamp(CurrentAimAngle, -180, 180);
            }
            else
            {
                CurrentAimAngle = Mathf.Clamp(CurrentAimAngle, -180, 180);
            }

            // applying angle to weapon rotation
            lookRotation = Quaternion.Euler(CurrentAimAngle * Vector3.forward);
            transform.rotation = lookRotation;

            // keep the last rotation
            lastRotation = transform.rotation;
        }
        else if(UIManager.GameIsPaused == true)
        {
            transform.rotation = lastRotation;
        }
        else
        {
            CurrentAimAngle = 0f;
            transform.rotation = initialRotation;
        }
    }

    private void EnemyAim()
    {
        currentAimAbsolute = currentAim;
        currentAim = weapon.WeaponOwner.GetComponent<CharacterFlip>().FacingRight ? currentAim : -currentAim;
        direction = currentAim - transform.position;
    }

    public void SetAim(Vector2 newAim)
    {
        currentAim = newAim;
    }

    // makes sure rotation is normal
    private void MoveReticle()
    {
        reticle.transform.rotation = Quaternion.identity;
        reticle.transform.position = reticlePosition;
    }

    public void DestroyReticle() {
        Destroy(reticle.gameObject);
    }
}
