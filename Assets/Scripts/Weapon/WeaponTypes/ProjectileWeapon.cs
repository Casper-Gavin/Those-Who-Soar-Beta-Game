using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : WeaponBase
{
    [SerializeField] private Vector3 projectileSpawnPosition;
    [SerializeField] private Vector3 projectileSpread;

    [Header("Weapon")]
    [SerializeField] private bool useMagazine = true;
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private bool reloadAutomatically = false;

    [Header("Effects")]
    [SerializeField] private ParticleSystem muzzlePS;

    public ObjectPooler Pooler { get; set; }

    public int MagazineSize => magazineSize;
    public bool UseMagazine => useMagazine;

    public int CurrentAmmo { get; set; }

    private Animator animator;
    private readonly int shootAnimationParameter = Animator.StringToHash("weaponUse");

    public WeaponAmmo WeaponAmmo { get; set; }

    [SerializeField] private AudioManager audioManager;

    protected override void Awake()
    {
        base.Awake();
        Pooler = GetComponent<ObjectPooler>();
        animator = GetComponent<Animator>();
        WeaponAmmo = GetComponent<WeaponAmmo>();

        audioManager = AudioManager.Instance;
    }

    protected override void Update() {
        base.Update();
    }

    // see if we can even make a request to attack
    public override void Attack()
    {
        if (useMagazine)
        {
            if (WeaponAmmo != null)
            {
                if (WeaponAmmo.CanUseWeapon())
                {
                    RequestAttack();
                }
                else if (reloadAutomatically)
                {
                    Reload(); // reload instantly, don't shoot
                }
            }
        }
        else
        {
            // if we are not using a magazine, then fire unconditionally
            RequestAttack();
        }
    }

    // attempt to shoot (may not be off cooldown)
    protected override void RequestAttack()
    {
        if (!audioManager.IsPlayingSFX("GunReload")) {
            /*
            // if we have no ammo and we are holding the fire button, reload
            if (CurrentAmmo <= 0 && Input.GetMouseButton(0)) {
                Reload();
                audioManager.PlaySFX("GunReload");
                return;
            }
            */

            if (OffAttackCooldown)
            {
                animator.SetTrigger(shootAnimationParameter);
                WeaponAmmo.ConsumeAmmo();
                if (WeaponOwner.CharacterTypes == Character.CharacterTypeEnum.Player)
                {
                    UIManager.Instance.UpdateAmmo(CurrentAmmo, magazineSize);
                }
                muzzlePS.Play();
                SpawnProjectile(DetermineProjectileSpawnPosition());
                OffAttackCooldown = false;
                nextAttackTime = Time.time + attackCooldown;

                audioManager.PlaySFX("GunShoot");
            }
        }
    }

    private Vector3 DetermineProjectileSpawnPosition()
    {
        bool right = WeaponOwner.GetComponent<CharacterFlip>().FacingRight;
        Vector3 leftSpawnPosition = new Vector3(projectileSpawnPosition.x, -projectileSpawnPosition.y, projectileSpawnPosition.z);
        return (right ? transform.position + transform.rotation * projectileSpawnPosition : transform.position - transform.rotation * leftSpawnPosition);
    }

    private void SpawnProjectile(Vector3 spawnPosition)
    {
        GameObject projectilePooled = Pooler.GetObjectFromPool();
        projectilePooled.transform.position = spawnPosition;
        projectilePooled.SetActive(true);

        Projectile projectile = projectilePooled.GetComponent<Projectile>();
        projectile.EnableProjectile();
        projectile.ProjectileOwner = WeaponOwner;

        Vector3 vectorSpread = new Vector3();
        vectorSpread.z = Random.Range(-projectileSpread.z, projectileSpread.z);
        Quaternion spread = Quaternion.Euler(vectorSpread);

        Vector2 newDirection = WeaponOwner.GetComponent<CharacterFlip>().FacingRight ? spread * transform.right : spread * transform.right * -1;
        projectile.SetDirection(newDirection, transform.rotation, WeaponOwner.GetComponent<CharacterFlip>().FacingRight);
    }

    public override void StopAttack()
    {

    }

    public override void EquipWeapon()
    {
        // use WeaponOwner
        if (WeaponOwner.CharacterTypes == Character.CharacterTypeEnum.Player)
        {
            UIManager.Instance.UpdateAmmo(CurrentAmmo, magazineSize);
            if (weaponUISprite != null)
            {
                UIManager.Instance.UpdateWeaponSprite(weaponUISprite);
            }
            else
            {
                UIManager.Instance.UpdateWeaponSprite(gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite);
            }
        }
    }

    public override void HolsterWeapon()
    {
        WeaponAmmo.SaveAmmo();
        Pooler.DestroyPool();
    }

    public override void Reload()
    {
        if (WeaponAmmo != null && !audioManager.IsPlayingSFX("GunReload") && CurrentAmmo < magazineSize)
        {
            if (useMagazine)
            {
                WeaponAmmo.RefillAmmo();
            }

            if (WeaponOwner.CharacterTypes == Character.CharacterTypeEnum.Player)
            {
                UIManager.Instance.UpdateAmmo(CurrentAmmo, magazineSize);

                audioManager.PlaySFX("GunReload");
            }
        }
    }
}