using UnityEngine;

public class PlayerHealth : HealthBase
{
    [Header("Shield")]
    [SerializeField] private float initialShield = 5f;
    [SerializeField] private float maxShield = 5f;
    
    private Character character;
    private PlayerController controller;
    private Collider2D collider2D;
    private SpriteRenderer[] spriteRenderers;
    private bool shieldBroken;
    public float CurrentShield { get; set; }

    // Awake is called before start
    protected override void Awake()
    {
        character = GetComponent<Character>(); // this should be a player
        controller = GetComponent<PlayerController>();
        collider2D = GetComponent<Collider2D>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(); // allow for any expansion of hierarchy
        CurrentHealth = initialHealth;
        CurrentShield = initialShield;

        UpdateHealth();
    }

    public override void TakeDamage(int damage)
    {
        if (CurrentHealth <= 0)
        {
            return;
        }

        if (!shieldBroken)
        {
            CurrentShield -= damage;
            CurrentShield = Mathf.Max(CurrentShield, 0); // prevent negative numbers
            UpdateHealth();
            if (CurrentShield <= 0)
            {
                shieldBroken = true;
            }
            return;
        }

        CurrentHealth -= damage;
        CurrentHealth = Mathf.Max(CurrentHealth, 0); // prevent negative numbers
        UpdateHealth();

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        if (character != null)
        {
            character.enabled = false;
            controller.enabled = false;
            collider2D.enabled = false;
            foreach (SpriteRenderer s in spriteRenderers)
            {
                s.enabled = false;
            }
        }

        if (destroyObject)
        {
            DestroyObject();
        }
    }

    public override void Revive()
    {
        if (character != null)
        {
            character.enabled = true;
            controller.enabled = true;
            collider2D.enabled = true;
            foreach (SpriteRenderer s in spriteRenderers)
            {
                s.enabled = true;
            }
        }

        gameObject.SetActive(true);
        CurrentHealth = initialHealth;
        CurrentShield = initialShield;
        shieldBroken = false;

        UpdateHealth();
        
    }

    public override void GainHealth(int amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);
        UpdateHealth();
    }

    public void GainShield(int amount)
    {
        CurrentShield = Mathf.Min(CurrentShield + amount, maxShield);
        UpdateHealth();
        if (CurrentShield > 0 && shieldBroken)
        {
            shieldBroken = false;
        }
    }

    protected override void UpdateHealth()
    {
        UIManager.Instance.UpdateHealth(CurrentHealth, maxHealth, CurrentShield, maxShield);
    }
}
