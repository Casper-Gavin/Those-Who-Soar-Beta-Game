using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb;

    [SerializeField] private float speed = 5f;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    // FixedUpdate is called once per physics update (0.02s) (multiple times per frame)
    void FixedUpdate() {
        MovePlayer();
    }

    /* Player Controller Functions */
    private void MovePlayer() {
        //rb.MovePosition(rb.position + movement * Time.fixedDeltaTime * speed);
    }
}
