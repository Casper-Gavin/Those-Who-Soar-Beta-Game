using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    [SerializeField] private GameObject reticlePrefab;

    private Camera mainCamera;
    private GameObject reticle;

    private Vector3 direction;
    private Vector3 mousePosition;
    private Vector3 reticlePosition;

    private void Start()
    {
        Cursor.visible = false;

        mainCamera = Camera.main;
        reticle = Instantiate(reticlePrefab);
    }

    private void Update()
    {
        GetMousePosition();
        MoveReticle();
    }

    // locks mouse position in the z direction to make sure that the program can always see the mouse
    // needs position in world units
    private void GetMousePosition()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = 5f;

        direction = mainCamera.ScreenToViewportPoint(mousePosition);
        direction.z = transform.position.z;
        reticlePosition = direction;
    }

    // makes sure rotation is normal
    private void MoveReticle()
    {
        reticle.transform.rotation = Quaternion.identity;
        reticle.transform.position = reticlePosition;
    }
}
