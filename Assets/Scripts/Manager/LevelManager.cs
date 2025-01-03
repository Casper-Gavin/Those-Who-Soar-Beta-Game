using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Character playableCharacter;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private GameObject bossHealthBarImage; // HealthContainer

    // private void Awake() {
    //     // Boss = GameObject.Find("Boss").transform;
    //     // Camera2D.Instance.Target = playableCharacter.transform;
    // }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.P))
        // {
        //     ReviveCharacter();
        // }
    }

    public void ReviveCharacter()
    {
        if (playableCharacter.GetComponent<PlayerHealth>().isDead)
        {
            bossHealthBarImage.SetActive(false);
            playableCharacter.transform.position = spawnPosition.position;
            playableCharacter.GetComponent<PlayerHealth>().Revive();
        }
    }
}
