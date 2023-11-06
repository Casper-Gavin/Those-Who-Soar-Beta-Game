using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Camera2DShake : MonoBehaviour {
    [SerializeField] private float shakeVibrato = 1.2f;
    [SerializeField] private float shakeRandomness = 0.05f;
    [SerializeField] private float shakeTime = 0.01f;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            Shake();
        }
    }

    public void Shake() {
        StartCoroutine(IEShake());
    }

    private IEnumerator IEShake() {
        Vector3 currentPosition = transform.position;

        for (int i = 0; i < shakeVibrato; i++) {
            Vector3 shakePosition = currentPosition + Random.insideUnitSphere * shakeRandomness * 0.5f;
            yield return new WaitForSeconds(shakeTime);
            transform.position = shakePosition;
        }
    }

    private void OnShooting() {
        Shake();
    }

    private void OnEnable() {
        CharacterWeapon.OnStartShooting += OnShooting;
    }

    private void OnDisable() {
        CharacterWeapon.OnStartShooting -= OnShooting;
    }
}
