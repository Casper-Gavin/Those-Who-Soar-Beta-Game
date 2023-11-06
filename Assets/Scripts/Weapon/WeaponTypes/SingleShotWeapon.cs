using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotWeapon : Weapon {
    [SerializeField] private Vector3 projectileSpawnPosition;

    public Vector3 ProjectileSpawnPosition { get; set; }

    public ObjectPooler Pooler { get; set; }

    private Vector3 projectileSpawnValue;

    private void Start() {
        projectileSpawnValue = projectileSpawnPosition;
        projectileSpawnValue.y = -projectileSpawnPosition.y;

        Pooler = GetComponent<ObjectPooler>();
    }

    protected override void Update() {
        base.Update();
    }

    protected override void RequestShot() {
        base.RequestShot();

        if (CanShoot) {
            EvaluateProjetileSpawnPosition();
            SpawnProjectile(ProjectileSpawnPosition);
        }
    }

    private void SpawnProjectile(Vector2 spawnPosition) {
        GameObject projectilePooled = Pooler.GetObjectFromPool();
        projectilePooled.transform.position = spawnPosition;
        projectilePooled.SetActive(true);

        Projectile projectile = projectilePooled.GetComponent<Projectile>();

        Vector2 newDirection = WeaponOwner.GetComponent<CharacterFlip>().FacingRight ? transform.right : transform.right * -1;
        projectile.SetDireciton(newDirection, transform.rotation, WeaponOwner.GetComponent<CharacterFlip>().FacingRight);

        CanShoot = false;
    }

    private void EvaluateProjetileSpawnPosition() {
        if (WeaponOwner.GetComponent<CharacterFlip>().FacingRight) {
            ProjectileSpawnPosition = transform.position + transform.rotation * projectileSpawnPosition;
        } else {
            ProjectileSpawnPosition = transform.position - transform.rotation * projectileSpawnValue;
        }
    }

    private void OnDrawGizmosSelected() {
        EvaluateProjetileSpawnPosition();

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(ProjectileSpawnPosition, 0.1f);
    }
}
