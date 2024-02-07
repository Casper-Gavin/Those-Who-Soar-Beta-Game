using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private Transform thisTransform;

    private float speed;
    private float angle;
    private float acceleration;

    private void Awake() {
        thisTransform = transform;
    }

    private void Update() {
        MoveProjectile();
    }

    public void Shoot(float newAngle, float newSpeed, float newAcceleration) {
        angle = newAngle;
        speed = newSpeed;
        acceleration = newAcceleration;

        // set the NEW rotation of the projectile
        Vector3 projectileAngle = thisTransform.eulerAngles;
        thisTransform.rotation = Quaternion.Euler(projectileAngle.x, projectileAngle.y, newAngle);
    }

    private void MoveProjectile() {
        Vector3 projectileAngle = thisTransform.rotation.eulerAngles;
        Quaternion newRotation = thisTransform.rotation;

        // set rotation of projectile
        float angleToAdd = acceleration * Time.deltaTime;
        // * change in z
        newRotation = Quaternion.Euler(projectileAngle.x, projectileAngle.y, projectileAngle.z + angleToAdd);

        // use acceleration
        speed += acceleration * Time.deltaTime;

        // move
        Vector3 newPosition = thisTransform.position + thisTransform.up * (speed * Time.deltaTime);

        //final movement
        thisTransform.SetPositionAndRotation(newPosition, newRotation);
    }
}