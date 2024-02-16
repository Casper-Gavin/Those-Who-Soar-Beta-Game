using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour {
    [Header("Collectable Settings")]
    [SerializeField] private bool canDestroyItem = true;
    [SerializeField] private bool canBounce = true;
    [SerializeField] private float timeBetweenBounces = 2.0f;
    [SerializeField] private float bounceDuration = 0.25f;
    [SerializeField] private float bounceDistance = 0.15f;

    protected Character character;
    protected GameObject objectCollided;
    protected SpriteRenderer spriteRenderer;
    protected Collider2D collider2D;

    protected float timeForNextBounce = 0.0f;
    protected bool lerp = false;
    protected float targetY = 0.0f;
    protected bool flippedY = false;
    protected float timeAccumulated = 0.0f;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
        timeForNextBounce = Time.time + timeBetweenBounces;
    }

    protected void Update()
    {
        if (canBounce)
        {
            if (Time.time > timeForNextBounce)
            {
                // trigger bounce
                lerp = true;
                flippedY = false;
                timeForNextBounce = Time.time + timeBetweenBounces;
                targetY = transform.position.y + bounceDistance;
                timeAccumulated = 0.0f;
            }
            else if (lerp)
            {
                timeAccumulated += Time.deltaTime;
                // enact bounce
                float halfDuration = bounceDuration / 2.0f;
                transform.position = new Vector3(transform.position.x, 
                                                 Mathf.Lerp(transform.position.y, targetY, timeAccumulated / halfDuration),
                                                 transform.position.z);

                if (transform.position.y == targetY)
                {
                    if (flippedY)
                    {
                        lerp = false;
                    }
                    flippedY = true;
                    targetY -= bounceDistance;
                    timeAccumulated = 0.0f;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        objectCollided = other.gameObject;
        if (IsPickable()) {
            Pick();
            PlayEffects();

            if (canDestroyItem) {
                Destroy(gameObject);
            } else {
                spriteRenderer.enabled = false;
                collider2D.enabled = false;
            }
        }
    }

    protected virtual bool IsPickable() {
        character = objectCollided.GetComponent<Character>();
        if (character == null) {
            return false;
        }

        return character.CharacterTypes == Character.CharacterTypeEnum.Player;
    }

    protected virtual void Pick(){

    }

    protected virtual void PlayEffects() {

    }
}
