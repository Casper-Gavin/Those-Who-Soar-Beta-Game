using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Camera2D : Singleton<Camera2D> {
    private enum CameraMode {
        Update,
        FixedUpdate,
        LateUpdate
    }

    private enum CameraFollow {
        FollowPlayer,
        FollowTarget,
        TransitionToVendor,
    }

    // public Transform Target {get; set;}

    [Header("Target")]
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Transform vendorTransform;
    [SerializeField] private Transform bossTransform;

    [Header("Offset")]
    [SerializeField] private Vector2 playerOffset;
    [SerializeField] private Vector2 vendorOffset;
    [SerializeField] private Vector2 bossOffset;

    [Header("Mode")]
    [SerializeField] private CameraMode cameraMode = CameraMode.Update;
    [SerializeField] private CameraFollow cameraFollow = CameraFollow.FollowPlayer;

    [SerializeField] private float dampingFactor = 1.35f;

    /// <summary>
    /// Should we make the camera dampening dependent on the player's velocity?
    /// For example, if the player is moving fast, the camera will follow the player faster
    /// and if the player is moving slow, the camera will follow the player slower.
    /// </summary>

    [Header("Target")]
    [SerializeField] private Vendor vendor;
    [SerializeField] private BossDetect bossDetect;
    [SerializeField] private GameObject boss;
    [SerializeField] private float smoothSpeed = 1.5f;
    [SerializeField] [Range(0f, 4f)] private float vendorZoomOut = 0.5f;
    [SerializeField] [Range(0f, 4f)] private float bossZoomOut = 0.25f;
    private float originalZoom;
    private Vector3 currentVelocity;
    private bool isTransitionComplete = false;


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

        if (bossDetect != null) {
            bossDetect.OnPlayerEnterBossZone.AddListener(TransitionToVendor);
            bossDetect.OnPlayerExitBossZone.AddListener(TransitionToPlayer);
        }
    }

    private void OnDisable() {
        if (vendor != null) {
            vendor.OnPlayerEnterShopZone.RemoveListener(TransitionToVendor);
            vendor.OnPlayerExitShopZone.RemoveListener(TransitionToPlayer);
        }

        if (bossDetect != null) {
            bossDetect.OnPlayerEnterBossZone.RemoveListener(TransitionToVendor);
            bossDetect.OnPlayerExitBossZone.RemoveListener(TransitionToPlayer);
        }
    }

    private void Update() {
        if (cameraMode == CameraMode.Update) {
            switch (cameraFollow) {
                case CameraFollow.FollowPlayer:
                    FollowTarget(targetTransform, playerOffset); // was targetTransform - switch back if doesn't work
                    break;
                case CameraFollow.FollowTarget:
                    if (vendor.vendorTag == "Vendor") {
                        if (vendorTransform != null) {
                            FollowBetweenPlayerAndVendor(targetTransform, vendorTransform, playerOffset, vendorOffset, vendorZoomOut);
                        } else {
                            // Get the vendor transform
                            GameObject vendorGameObject = GameObject.FindGameObjectWithTag("Vendor"); // THIS IS A VERY EXPENSIVE OPERATION, MIGHT WANT TO OPTIMIZE
                            vendorTransform = vendorGameObject.GetComponent<Transform>();
                        }
                    } else if (bossDetect.bossTag == "Boss") {
                        if (boss != null) {
                            FollowBetweenPlayerAndBoss(targetTransform, bossTransform, playerOffset, bossOffset, bossZoomOut);
                        } else {
                            // Get the boss transform
                            boss = GameObject.FindGameObjectWithTag("Boss"); // THIS IS A VERY EXPENSIVE OPERATION, MIGHT WANT TO OPTIMIZE
                            bossTransform = boss.GetComponent<Transform>();
                        }
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
                case CameraFollow.FollowTarget:
                    if (vendor.vendorTag == "Vendor") {
                        if (vendorTransform != null) {
                            FollowBetweenPlayerAndVendor(targetTransform, vendorTransform, playerOffset, vendorOffset, vendorZoomOut);
                        } else {
                            // Get the vendor transform
                            GameObject vendorGameObject = GameObject.FindGameObjectWithTag("Vendor"); // THIS IS A VERY EXPENSIVE OPERATION, MIGHT WANT TO OPTIMIZE
                            vendorTransform = vendorGameObject.GetComponent<Transform>();
                        }
                    } else if (bossDetect.bossTag == "Boss") {
                        if (boss != null) {
                            FollowBetweenPlayerAndBoss(targetTransform, bossTransform, playerOffset, bossOffset, bossZoomOut);
                        } else {
                            // Get the boss transform
                            boss = GameObject.FindGameObjectWithTag("Boss"); // THIS IS A VERY EXPENSIVE OPERATION, MIGHT WANT TO OPTIMIZE
                            bossTransform = boss.GetComponent<Transform>();
                        }
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
                case CameraFollow.FollowTarget:
                    if (vendor.vendorTag == "Vendor") {
                        if (vendorTransform != null) {
                            FollowBetweenPlayerAndVendor(targetTransform, vendorTransform, playerOffset, vendorOffset, vendorZoomOut);
                        } else {
                            // Get the vendor transform
                            GameObject vendorGameObject = GameObject.FindGameObjectWithTag("Vendor"); // THIS IS A VERY EXPENSIVE OPERATION, MIGHT WANT TO OPTIMIZE
                            vendorTransform = vendorGameObject.GetComponent<Transform>();
                        }
                    } else if (bossDetect.bossTag == "Boss") {
                        if (boss != null) {
                            FollowBetweenPlayerAndBoss(targetTransform, bossTransform, playerOffset, bossOffset, bossZoomOut);
                        } else {
                            // Get the boss transform
                            boss = GameObject.FindGameObjectWithTag("Boss"); // THIS IS A VERY EXPENSIVE OPERATION, MIGHT WANT TO OPTIMIZE
                            bossTransform = boss.GetComponent<Transform>();
                        }
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

    /*private void OnTriggerEnter2D (Collider2D other) {
        if (other.CompareTag("Vendor")) {
            vendorTransform = other.GetComponent<Transform>();
            if (cameraFollow == CameraFollow.FollowPlayer) {
                TransitionToVendor();
            }
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        if (other.CompareTag("Vendor")) {
            if (cameraFollow == CameraFollow.FollowTarget) {
                TransitionToPlayer();
                vendorTransform = null;
            }
        }
    }*/

    public void SnapToTarget(Transform target)
    {
        Vector3 newPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = newPosition;
    }

    private void FollowTarget(Transform target, Vector2 offset) {
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originalZoom, Time.deltaTime * smoothSpeed);
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        Vector3 dampenedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * dampingFactor);
        transform.position = dampenedPosition;
    }

    private void FollowBetweenPlayerAndVendor(Transform player, Transform target, Vector2 playerOffset, Vector2 targetOffset, float zoomOut) {
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originalZoom * zoomOut, Time.deltaTime * smoothSpeed);
        Vector3 desiredPosition = new Vector3(((player.position.x + playerOffset.x) + (target.position.x + targetOffset.x)) / 2, ((player.position.y + playerOffset.y) + (target.position.y + targetOffset.y)) / 2, transform.position.z);
        Vector3 dampenedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * dampingFactor);
        transform.position = dampenedPosition;
    }

    private void FollowBetweenPlayerAndBoss(Transform player, Transform target, Vector2 playerOffset, Vector2 targetOffset, float zoomOut) {
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originalZoom * zoomOut, Time.deltaTime * smoothSpeed);
        Vector3 desiredPosition = new Vector3(((player.position.x + playerOffset.x) + (target.position.x + targetOffset.x)) / 2, ((player.position.y + playerOffset.y) + (target.position.y + targetOffset.y)) / 2, transform.position.z);
        Vector3 dampenedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * dampingFactor);
        transform.position = dampenedPosition;
    }

    private void TransitionCamera(Transform target, Vector2 offset) {
        if (vendorTransform != null && !isTransitionComplete) {
            TransitionVendor(target, offset);
        } else if (bossTransform != null && !isTransitionComplete) {
            TransitionBoss(target, offset);
        }
    }

    private void TransitionVendor(Transform target, Vector2 offset) {
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        Vector3 dampenedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * dampingFactor);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originalZoom * vendorZoomOut, Time.deltaTime * smoothSpeed);

        float distance = Vector3.Distance(transform.position, desiredPosition);
        if (distance < 0.01f) {
            // Transition complete, switch to FollowTarget mode
            isTransitionComplete = true;
            cameraFollow = CameraFollow.FollowTarget;
        }

        transform.position = dampenedPosition;
    }

    private void TransitionBoss(Transform target, Vector2 offset) {
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        Vector3 dampenedPosition = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * dampingFactor);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, originalZoom * bossZoomOut, Time.deltaTime * smoothSpeed);

        float distance = Vector3.Distance(transform.position, desiredPosition);
        if (distance < 0.01f) {
            // Transition complete, switch to FollowTarget mode
            isTransitionComplete = true;
            cameraFollow = CameraFollow.FollowTarget;
        }

        transform.position = dampenedPosition;
    }

    private void TransitionToVendor() {
        if (cameraFollow == CameraFollow.FollowPlayer) {
            isTransitionComplete = false;
            cameraFollow = CameraFollow.FollowTarget;
            //cameraFollow = CameraFollow.TransitionToVendor;
            //StartCoroutine(TransitionDelay());
        }
    }

    private void TransitionToPlayer() {
        if (cameraFollow == CameraFollow.FollowTarget) {
            cameraFollow = CameraFollow.FollowPlayer;
        }
    }

    private IEnumerator TransitionDelay() {
        yield return new WaitForSeconds(0.2f); // Add a small delay to avoid camera jitter
        cameraFollow = CameraFollow.FollowTarget;
    }

    private IEnumerator ChangeDampingFactor(float differentDampingFactor = 0.1f) {
        dampingFactor = differentDampingFactor;
        yield return new WaitForSeconds(0.65f);
        dampingFactor = originalDampingFactor;
    }
}