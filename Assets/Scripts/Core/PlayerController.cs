using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;

    public Vector2 CurrentMovement { get; set; }

    public bool NormalMovement { get; set; }

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();

        NormalMovement = true;
    }

    // Update is called once per frame
    void Update() {
        
    }

    // FixedUpdate is called once per physics update (0.02s) (multiple times per frame)
    void FixedUpdate() {
        if (NormalMovement) {
            MovePlayer();
        }
    }



    private void MovePlayer() {
        Vector2 currentMovement = rb.position + CurrentMovement * Time.fixedDeltaTime;
        rb.MovePosition(currentMovement);
    }

    public void MovePosition(Vector2 newPosition) {
        rb.MovePosition(newPosition);
    }

    public void SetMovement(Vector2 newPosition) {
        CurrentMovement = newPosition;
    }
}
