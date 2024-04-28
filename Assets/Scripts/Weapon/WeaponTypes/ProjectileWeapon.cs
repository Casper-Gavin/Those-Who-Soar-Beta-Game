using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : WeaponBase
{
    [SerializeField] private Vector3 projectileSpawnPosition;
    [SerializeField] private Vector3 projectileSpread;

    [Header("Weapon")]
    [SerializeField] private bool useMagazine = true;
    [SerializeField] private int magazineSize = 15;
    [SerializeField] private bool reloadAutomatically = false;

    [Header("Effects")]
    [SerializeField] private ParticleSystem muzzlePS;

    public ObjectPooler Pooler { get; set; }

    public int CurrentAmmo { get; set; }

    private Animator animator;
    private readonly int shootAnimationParameter = Animator.StringToHash("weaponUse");

    [SerializeField] private AudioManager audioManager;

    protected override void Awake()
    {
        //attackCooldown = 0.5f;

        base.Awake();
        Pooler = GetComponent<ObjectPooler>();
        animator = GetComponent<Animator>();
        CurrentAmmo = magazineSize;
        audioManager = AudioManager.Instance;
    }

    protected override void Update() {
        base.Update();
    }

    private bool CanUseWeapon()
    {
        return CurrentAmmo > 0 && UIManager.GameIsPaused == false;
    }

    // see if we can even make a request to attack
    public override void Attack()
    {
        if (useMagazine)
        {
            if (CanUseWeapon())
            {
                RequestAttack();
            }
            else if (reloadAutomatically)
            {
                Reload(); // reload instantly, don't shoot
            }
        }
        else
        {
            // if we are not using a magazine, then fire unconditionally
            RequestAttack();
        }
    }

    private void ConsumeAmmo()
    {
        if (useMagazine)
        {
            CurrentAmmo -=1;
        }
    }

    // attempt to shoot (may not be off cooldown)
    protected override void RequestAttack()
    {
        if (!audioManager.IsPlayingSFX("GunReload"))
        {
            if (OffAttackCooldown)
            {
                animator.SetTrigger(shootAnimationParameter);
                ConsumeAmmo();
                if (WeaponOwner.CharacterTypes == Character.CharacterTypeEnum.Player)
                {
                    UIManager.Instance.UpdateAmmo(CurrentAmmo, magazineSize);
                }
                muzzlePS.Play();
                SpawnProjectile(DetermineProjectileSpawnPosition());
                OffAttackCooldown = false;
                nextAttackTime = Time.time + attackCooldown;

                audioManager.MakeAndPlaySFX("GunShoot");
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
                nextAttackTime = Time.time + attackCooldown;
            }
            else
            {
                UIManager.Instance.UpdateWeaponSprite(gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite);
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    public override void HolsterWeapon()
    {
    }

    private void OnDestroy()
    {
        Pooler.DestroyPool();
    }

    private void RefillAmmo()
    {
        if (useMagazine && UIManager.GameIsPaused == false)
        {
            CurrentAmmo = magazineSize;
        }
    }

    public override void Reload()
    {
        if (!audioManager.IsPlayingSFX("GunReload") && CurrentAmmo < magazineSize)
        {
            if (useMagazine)
            {
                RefillAmmo();
            }

            if (WeaponOwner.CharacterTypes == Character.CharacterTypeEnum.Player)
            {
                UIManager.Instance.UpdateAmmo(CurrentAmmo, magazineSize);

                audioManager.PlaySFX("GunReload");
            }
        }
    }

    public override List<object> GetWeaponInformation()
    {
        List<object> info = new List<object>();
        info.Add(CurrentAmmo);
        return info;
    }

    public override void SetWeaponInformation(List<object> weaponInformation)
    {
        if (weaponInformation == null || weaponInformation.Count == 0)
        {
            CurrentAmmo = magazineSize;
        }
        else
        {
            CurrentAmmo = (int) weaponInformation[0];
        }
    }

}