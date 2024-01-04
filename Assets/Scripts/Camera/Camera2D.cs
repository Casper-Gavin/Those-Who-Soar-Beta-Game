using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2D : MonoBehaviour {
    private enum CameraMode {
        Update,
        FixedUpdate,
        LateUpdate
    }

    private enum CameraFollow {
        FollowPlayer,
        FollowVendor,
        TransitionToVendor
    }

    [Header("Target")]
    [SerializeField] private Transform targetTransform;

    [Header("Offset")]
    [SerializeField] private Vector2 playerOffset;
    [SerializeField] private Vector2 vendorOffset;

    [Header("Mode")]
    [SerializeField] private CameraMode cameraMode = CameraMode.Update;
    [SerializeField] private CameraFollow cameraFollow = CameraFollow.FollowPlayer;

    [SerializeField] private float dampingFactor = 0.75f;

    [Header("Vendor")]
    [SerializeField] private Vendor vendor;
    [SerializeField] private float smoothSpeed = 2.5f;
    private Vector3 currentVelocity;
    private bool isTransitionComplete = false;
    private Transform vendorTransform;

    private void OnEnable() {
        if (vendor != null) {
            vendor.OnPlayerEnterShopZone.AddListener(TransitionToVendor);
            vendor.OnPlayerExitShopZone.AddListener(TransitionToPlayer);
        }
    }

    private void OnDisable() {
        if (vendor != null) {
            vendor.OnPlayerEnterShopZone.RemoveListener(TransitionToVendor);
            vendor.OnPlayerExitShopZone.RemoveListener(TransitionToPlayer);
        }
    }

    private void Update() {
        if (cameraMode == CameraMode.Update) {
            switch (cameraFollow) {
                case CameraFollow.FollowPlayer:
                    FollowTarget(targetTransform, playerOffset);
                    break;
                case CameraFollow.FollowVendor:
                    if (vendorTransform != null) {
                        FollowTarget(vendorTransform, vendorOffset);
                    }
                    break;
                case CameraFollow.TransitionToVendor:
                    TransitionCamera(vendorTransform, vendorOffset);
                    break;
            }
        }
    }

    private void FixedUpdate() {
        if (cameraMode == CameraMode.FixedUpdate) {
            switch (cameraFollow) {
                case CameraFollow.FollowPlayer:
                    FollowTarget(targetTransform, playerOffset);
                    break;
                case CameraFollow.FollowVendor:
                    if (vendorTransform != null) {
                        FollowTarget(vendorTransform, vendorOffset);
                    }
                    break;
                case CameraFollow.TransitionToVendor:
                    TransitionCamera(vendorTransform, vendorOffset);
                    break;
            }
        }
    }

    private void LateUpdate() {
        if (cameraMode == CameraMode.LateUpdate) {
            switch (cameraFollow) {
                case CameraFollow.FollowPlayer:
                    FollowTarget(targetTransform, playerOffset);
                    break;
                case CameraFollow.FollowVendor:
                    if (vendorTransform != null) {
                        FollowTarget(vendorTransform, vendorOffset);
                    }
                    break;
                case CameraFollow.TransitionToVendor:
                    TransitionCamera(vendorTransform, vendorOffset);
                    break;
            }
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag("Vendor")) {
            vendorTransform = other.GetComponent<Transform>();
            if (cameraFollow == CameraFollow.FollowPlayer) {
                TransitionToVendor();
            }
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        if (other.CompareTag("Vendor")) {
            if (cameraFollow == CameraFollow.FollowVendor) {
                TransitionToPlayer();
            }
        }
    }

    private void FollowTarget(Transform target, Vector2 offset) {
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        Vector3 dampenedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * dampingFactor);
        transform.position = dampenedPosition;

        /*if (!vendor.canOpenShop) {
            Vector3 desiredPosition = new Vector3(targetTransform.position.x + offset.x, targetTransform.position.y + offset.y, transform.position.z);
            //transform.position = desiredPosition;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        } else {
            Vector3 desiredPosition = new Vector3(targetTransform.position.x + offset.x, targetTransform.position.y + offset.y + 3f, transform.position.z);
            //transform.position = desiredPosition;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }*/
    }

    /*private void ShiftCameraUp() {
        if (!isTransitioning) {
            Debug.Log("Shifting camera up");
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
            //transform.position = newPosition;
            StartCoroutine(SmoothTransition(newPosition));
        }
    }

    private void ResetCameraPosition() {
        if (!isTransitioning) {
            Debug.Log("Resetting camera position");
            Vector3 targetPosition = new Vector3(targetTransform.position.x + offset.x, targetTransform.position.y + offset.y, transform.position.z);
            //transform.position = newPosition;
            StartCoroutine(SmoothTransition(targetPosition));
        }
    }

    private IEnumerator SmoothTransition(Vector3 newPosition) {
        isTransitioning = true;

        while (Vector3.Distance(transform.position, newPosition) > 0.01f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref currentVelocity, smoothSpeed * Time.deltaTime);
            yield return null;
        }
        
        isTransitioning = false;
    }*/

    private void TransitionCamera(Transform target, Vector2 offset) {
        if (vendorTransform != null && !isTransitionComplete) {
            Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
            Vector3 dampenedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * dampingFactor);

            float distance = Vector3.Distance(transform.position, desiredPosition);
            if (distance < 0.01f) {
                // Transition complete, switch to FollowVendor mode
                isTransitionComplete = true;
                cameraFollow = CameraFollow.FollowVendor;
            }

            transform.position = dampenedPosition;
        }
    }

    private void TransitionToVendor() {
        if (cameraFollow == CameraFollow.FollowPlayer) {
            isTransitionComplete = false;
            cameraFollow = CameraFollow.TransitionToVendor;
            StartCoroutine(TransitionDelay());
        }
    }

    private void TransitionToPlayer() {
        if (cameraFollow == CameraFollow.FollowVendor) {
            cameraFollow = CameraFollow.FollowPlayer;
        }
    }

    private IEnumerator TransitionDelay() {

        yield return new WaitForSeconds(0.2f); // Add a small delay to avoid camera jitter
        cameraFollow = CameraFollow.FollowVendor;
    }
}