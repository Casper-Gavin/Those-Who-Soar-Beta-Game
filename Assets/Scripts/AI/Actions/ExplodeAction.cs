using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Explode", fileName = "ExplodeAction")]
public class ExplodeAction : AIAction
{
    public Color flashColor = Color.red; // You must change this in unity for change to take effect
    public float flashDuration = 0.5f; // You must change this in unity for change to take effect
    public int numberOfFlashes = 4; // You must change this in unity for change to take effect
    public float explosionRadius = 5f; // You must change this in unity for change to take effect
    public float explosionDamage = 5f; // You must change this in unity for change to take effect
    public GameObject explosionEffectPrefab;

    public SpriteRenderer spriteRenderer;
    private Color originalColor;
    private int currentFlashCount = 0;
    private float flashTimeAccumulator = 0f;
    private EnemyHealth bomberHealth;

    public override void Init(StateController controller)
    {
        currentFlashCount = 0;
        flashTimeAccumulator = 0f;
        spriteRenderer = controller.transform.GetChild(1).GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        controller.CharacterMovement.SetHorizontal(0);
        controller.CharacterMovement.SetVertical(0);
        bomberHealth = controller.GetComponentInChildren<EnemyHealth>();
    }


    public override void Act(StateController controller)
    {
        CheckExplode(controller);
    }

    private void CheckExplode(StateController controller)
    {
        if (controller.Target == null)
        {
            Debug.Log("null target");
            return;
        }

        if (CanFlash())
        {
            Debug.Log("flashing");
            Flash(controller);
        }
        else /* some logic here */
        {
            Debug.Log("original color = " + originalColor.ToString() + " flash color = " + flashColor.ToString());
            flashTimeAccumulator += Time.deltaTime;
            if (flashTimeAccumulator < (flashDuration / 2))
            {
                spriteRenderer.color = Color.Lerp(originalColor, flashColor, flashTimeAccumulator / (flashDuration / 2));
            }
            else
            {
                spriteRenderer.color = Color.Lerp(flashColor, originalColor, flashTimeAccumulator / (flashDuration / 2));
            }
        }
    }

    private bool CanFlash() 
    {
        Debug.Log("current flash count " + currentFlashCount);
        Debug.Log("number of flashes " + numberOfFlashes);
        if (currentFlashCount < numberOfFlashes && flashTimeAccumulator >= flashDuration)
        {
            return true;
        }
        return false;
    }

    private void Flash(StateController controller)
    {
        Debug.Log("flash ");
        currentFlashCount++;
        flashTimeAccumulator = 0f;
        if (currentFlashCount == numberOfFlashes)
        {
            Explode(controller);
        }
    }

    private void Explode(StateController controller)
    {
        // Instantiate the explosion effect
        if (explosionEffectPrefab)
        {
            Instantiate(explosionEffectPrefab, controller.transform.position, Quaternion.identity);
        }

        // See if player is one of the colliders within the explosion radius
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(controller.transform.position, explosionRadius))
        {
            collider.GetComponent<PlayerHealth>()?.TakeDamage((int)explosionDamage);
        }

        bomberHealth.TakeDamage((int)bomberHealth.CurrentHealth + 1); // make bomber kill himself without having to change access modifier on Die() or DestroyObject() functions
    }
}
