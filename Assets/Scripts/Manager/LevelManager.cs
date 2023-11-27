using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Character playableCharacter;
    [SerializeField] private Transform spawnPosition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ReviveCharacter();
        }
    }

    private void ReviveCharacter()
    {
        if (playableCharacter.GetComponent<PlayerHealth>().CurrentHealth <= 0)
        {
            playableCharacter.GetComponent<PlayerHealth>().Revive();
            playableCharacter.transform.position = spawnPosition.position;
        }
    }
}
