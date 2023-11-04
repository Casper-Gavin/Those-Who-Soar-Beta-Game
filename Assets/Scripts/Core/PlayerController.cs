using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // myRigidBody2D in tutorial = rb
    private Rigidbody2D rb;
    private Vector2 recoilMovement;

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
    void FixedUpdate()
    {
        Recoil();

        if (NormalMovement) {
            MovePlayer();
        }
    }

    private void MovePlayer() {
        Vector2 currentMovement = rb.position + CurrentMovement * Time.fixedDeltaTime;
        rb.MovePosition(currentMovement);
    }

    public void ApplyRecoil(Vector2 recoilDirection, float recoilForce)
    {
        // direction * magnitude
        recoilMovement = recoilDirection.normalized * recoilForce;
    }

    private void Recoil()
    {
        if (recoilMovement.magnitude > 0.1f)
        {
            rb.AddForce(recoilMovement);
        }
    }

    public void MovePosition(Vector2 newPosition) {
        rb.MovePosition(newPosition);
    }

    public void SetMovement(Vector2 newPosition) {
        CurrentMovement = newPosition;
    }

}
