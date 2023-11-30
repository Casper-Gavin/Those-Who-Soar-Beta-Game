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
    [SerializeField] private bool reloadAutomatically = true;

    [Header("Recoil")]
    [SerializeField] private bool recoilEnabled = true;
    [SerializeField] private int recoilForce = 5; // what are the units for this?

    [Header("Effects")]
    [SerializeField] private ParticleSystem muzzlePS;

    public ObjectPooler Pooler { get; set; }

    public int MagazineSize => magazineSize;
    public bool UseMagazine => useMagazine;

    public int CurrentAmmo { get; set; }

    private Animator animator;
    private readonly int shootAnimationParameter = Animator.StringToHash("weaponUse");

    public WeaponAmmo WeaponAmmo { get; set; }

    protected override void Awake()
    {
        base.Awake();
        Pooler = GetComponent<ObjectPooler>();
        animator = GetComponent<Animator>();
        WeaponAmmo = GetComponent<WeaponAmmo>();
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
        if (OffAttackCooldown)
        {
            if (recoilEnabled)
            {
                Recoil();
            }
            animator.SetTrigger(shootAnimationParameter);
            WeaponAmmo.ConsumeAmmo();
            if (WeaponOwner.CharacterTypes == Character.CharacterType.Player)
            {
                UIManager.Instance.UpdateAmmo(CurrentAmmo, magazineSize);
            }
            muzzlePS.Play();
            SpawnProjectile(DetermineProjectileSpawnPosition());
            OffAttackCooldown = false;
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
        if (recoilEnabled)
        {
            controller.ApplyRecoil(Vector2.one, 0f); // halt recoil
        }
    }

    public override void EquipWeapon()
    {
        // use WeaponOwner
        if (WeaponOwner.CharacterTypes == Character.CharacterType.Player)
        {
            UIManager.Instance.UpdateAmmo(CurrentAmmo, magazineSize);
            // this implies that we must have sprite as first child
            UIManager.Instance.UpdateWeaponSprite(gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite);
        }
    }

    public override void HolsterWeapon()
    {
        WeaponAmmo.SaveAmmo();
    }

    private void Recoil()
    {
        if (WeaponOwner != null)
        {
            bool right = WeaponOwner.GetComponent<CharacterFlip>().FacingRight;
            controller.ApplyRecoil(right ? Vector2.left : Vector2.right, recoilForce);
        }
    }

    public override void Reload()
    {
        if (WeaponAmmo != null)
        {
            if (useMagazine)
            {
                WeaponAmmo.RefillAmmo();
            }

            if (WeaponOwner.CharacterTypes == Character.CharacterType.Player)
            {
                UIManager.Instance.UpdateAmmo(CurrentAmmo, magazineSize);
            }
        }
    }
}