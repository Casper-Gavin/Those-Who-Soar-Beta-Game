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

    private Vector2 movement;

    void Awake () {
        Speed = speed;
        FacingRight = true;

        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate() {
        MoveProjectile();
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

    public void SetDireciton(Vector2 newDirection, Quaternion newRotation, bool isFacingRight) {
        Direction = newDirection;

        if (FacingRight != isFacingRight) {
            FlipProjectile();
        }

        transform.rotation = newRotation;
    }

    public void ResetProjectile() {
        spriteRenderer.flipX = false;
    }
}
