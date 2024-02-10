using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SkillMenu;

public class EnemyHealth : HealthBase
{
    [SerializeField] protected GameObject enemyHealthBarPrefab;
    [SerializeField] protected Vector3 offset = new Vector3(0f, 1f, 0f);
    [SerializeField] protected int damageTakenFromBullet = 1;

    protected Image healthBarImage;
    protected GameObject gameObjectHealthBar;

    //protected SkillMenu skillMenu;

    protected override void Awake() // this used to be Start(), it is apparently very weird to use Start() with MonoBehavior inheritance
    {
        // if enemy is boss then this won't be used
        if (enemyHealthBarPrefab != null) {
            // Instantiate is used to create game objects (prefabs) on the fly
            gameObjectHealthBar = Instantiate(enemyHealthBarPrefab, transform.position + offset, Quaternion.identity);
            gameObjectHealthBar.transform.parent = transform;
            healthBarImage = gameObjectHealthBar.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        }

        CurrentHealth = initialHealth;

        //skillMenu = SkillMenu.skillMenu;
    }

    protected override void Update()
    {
        UpdateHealth();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(damageTakenFromBullet);
        }
    }

    public override void TakeDamage(int damage) {
        if (CurrentHealth <= 0)
        {
            return;
        }

        MeleeAttack meleeAttack = GameObject.Find("Player").GetComponentInChildren<MeleeAttack>();
        CurrentHealth -= meleeAttack.damageToEnemy;
        
        //if (skillMenu.skillLevels[(int)SkillEnum.IncreaseDamage] > 0)
        //{
        //    CurrentHealth -= skillMenu.skillLevels[(int)SkillEnum.IncreaseDamage];
        //}
        
        //CurrentHealth = Mathf.Max(CurrentHealth, 0); // prevent negative numbers
        UpdateHealth();

        if (CurrentHealth <= 0) {
            Die();
        }
    }

    protected override void UpdateHealth()
    {
            if (healthBarImage != null) {
                healthBarImage.fillAmount = Mathf.Lerp(healthBarImage.fillAmount, CurrentHealth / maxHealth, 10f * Time.deltaTime);
            }
    }

    protected override void Die()
    {
        DestroyObject();
    }
}