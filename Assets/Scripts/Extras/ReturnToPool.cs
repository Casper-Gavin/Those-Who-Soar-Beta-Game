using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour {
    [SerializeField] private float lifeTime = 2f;

    private void Return () {
        gameObject.SetActive(false);
    }

    private void OnEnable() {
        Invoke(nameof(Return), lifeTime);
    }

    private void OnDisable() {
        CancelInvoke();
    }
}
