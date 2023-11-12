using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private float speed = 100f;
    [SerializeField] private float acceleration = 0f;

    public Vector2 Direction { get; set; }

    public bool FacingRight { get; set; }

    public float Speed { get; set; }

    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;
    private Vector2 movement;
    private bool canMove;

    void Awake () {
        Speed = speed;
        FacingRight = true;
        canMove = true;

        rigidbody2D = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate() {
        if (canMove)
        {
            MoveProjectile();
        }
    }

    public void MoveProjectile() {
        movement = Direction * (Speed / 10f) * Time.fixedDeltaTime;
        rigidbody2D.MovePosition(rigidbody2D.position + movement);

        Speed += acceleration * Time.deltaTime;
    }

    public void FlipProjectile() {
        if (spriteRenderer != null) {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    public void SetDirection(Vector2 newDirection, Quaternion newRotation, bool isFacingRight) {
        Direction = newDirection;

        if (FacingRight != isFacingRight) {
            FlipProjectile();
        }

        transform.rotation = newRotation;
    }

    public void ResetProjectile() {
        spriteRenderer.flipX = false;
    }

    public void DisableProjectile()
    {
        canMove = false;
        spriteRenderer.enabled = false;
        collider2D.enabled = false;
    }

    public void EnableProjectile()
    {
        canMove = true;
        spriteRenderer.enabled = true;
        collider2D.enabled = true;
    }
}
