using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    public void Kill()
    {
        StartCoroutine(DelayBeforeKill());
    }

    private IEnumerator DelayBeforeKill()
    {
        yield return new WaitForSeconds(0.1f); // doesn't work
        playerHealth?.Kill();
    }
}