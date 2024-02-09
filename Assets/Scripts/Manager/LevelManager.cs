using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Character playableCharacter;
    [SerializeField] private Transform spawnPosition;

    // private void Awake() {
    //     // Boss = GameObject.Find("Boss").transform;
    //     // Camera2D.Instance.Target = playableCharacter.transform;
    // }

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
