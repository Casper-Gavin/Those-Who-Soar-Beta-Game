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

    [SerializeField] private float dampingFactor = 1.35f;

    /// <summary>
    /// Should we make the camera dampening dependent on the player's velocity?
    /// For example, if the player is moving fast, the camera will follow the player faster
    /// and if the player is moving slow, the camera will follow the player slower.
    /// </summary>

    [Header("Vendor")]
    [SerializeField] private Vendor vendor;
    [SerializeField] private float smoothSpeed = 2.5f;
    [SerializeField] [Range(0f, 4f)] private float vendorZoomOut = 0.5f;
    private float originalZoom;
    private Vector3 currentVelocity;
    private bool isTransitionComplete = false;
    private Transform vendorTransform;

    [Header("Movement")]
    [SerializeField] private float dashDampingFactor = 2f; // Camera follows player closer when dashing
    private CharacterDash characterDash;
    float originalDampingFactor;
    
    private void Start() {
        originalZoom = Camera.main.orthographicSize;
        characterDash = targetTransform.GetComponent<CharacterDash>();
        originalDampingFactor = dampingFactor;
    }

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
                        FollowBetweenPlayerAndTarget(targetTransform, vendorTransform, playerOffset, vendorOffset, vendorZoomOut);
                    } else {
                        // Get the vendor transform
                        GameObject vendorGameObject = GameObject.FindGameObjectWithTag("Vendor"); // THIS IS A VERY EXPENSIVE OPERATION, MIGHT WANT TO OPTIMIZE
                        vendorTransform = vendorGameObject.GetComponent<Transform>();
                    }
                    break;
                case CameraFollow.TransitionToVendor:
                    TransitionCamera(vendorTransform, vendorOffset);
                    break;
            }

            if (characterDash.GetIsDashing()) {
                StartCoroutine(ChangeDampingFactor(dashDampingFactor));
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
                        FollowBetweenPlayerAndTarget(targetTransform, vendorTransform, playerOffset, vendorOffset, vendorZoomOut);
                    } else {
                        // Get the vendor transform
                        GameObject vendorGameObject = GameObject.FindGameObjectWithTag("Vendor"); // THIS IS A VERY EXPENSIVE OPERATION, MIGHT WANT TO OPTIMIZE
                        vendorTransform = vendorGameObject.GetComponent<Transform>();
                    }
                    break;
                case CameraFollow.TransitionToVendor:
                    TransitionCamera(vendorTransform, vendorOffset);
                    break;
            }

            if (characterDash.GetIsDashing()) {
                StartCoroutine(ChangeDampingFactor(dashDampingFactor));
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
                        FollowBetweenPlayerAndTarget(targetTransform, vendorTransform, playerOffset, vendorOffset, vendorZoomOut);
                    } else {
                        // Get the vendor transform
                        GameObject vendorGameObject = GameObject.FindGameObjectWithTag("Vendor"); // THIS IS A VERY EXPENSIVE OPERATION, MIGHT WANT TO OPTIMIZE
                        vendorTransform = vendorGameObject.GetComponent<Transform>();
                    }
                    break;
                case CameraFollow.TransitionToVendor:
                    TransitionCamera(vendorTransform, vendorOffset);
                    break;
            }

            if (characterDash.GetIsDashing()) {
                StartCoroutine(ChangeDampingFactor(dashDampingFactor));
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
                vendorTransform = null;
            }
        }
    }

    private void FollowTarget(Transform target, Vector2 offset) {
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originalZoom, Time.deltaTime * smoothSpeed);
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        Vector3 dampenedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * dampingFactor);
        transform.position = dampenedPosition;
    }

    private void FollowBetweenPlayerAndTarget(Transform player, Transform target, Vector2 playerOffset, Vector2 targetOffset, float zoomOut) {
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originalZoom * zoomOut, Time.deltaTime * smoothSpeed);
        Vector3 desiredPosition = new Vector3(((player.position.x + playerOffset.x) + (target.position.x + targetOffset.x)) / 2, ((player.position.y + playerOffset.y) + (target.position.y + targetOffset.y)) / 2, transform.position.z);
        Vector3 dampenedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * dampingFactor);
        transform.position = dampenedPosition;
    }

    private void TransitionCamera(Transform target, Vector2 offset) {
        if (vendorTransform != null && !isTransitionComplete) {
            Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
            Vector3 dampenedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * dampingFactor);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originalZoom * vendorZoomOut, Time.deltaTime * smoothSpeed);

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

    private IEnumerator ChangeDampingFactor(float differentDampingFactor = 0.1f) {
        dampingFactor = differentDampingFactor;
        yield return new WaitForSeconds(0.65f);
        dampingFactor = originalDampingFactor;
    }
}