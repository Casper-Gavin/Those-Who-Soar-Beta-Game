using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Key key;
    [SerializeField] private float distToDoorToOpen = 1.0f;
    private Character playerRef;
    private bool doorOpened = false;

    private void Start()
    {
        foreach (Character character in FindObjectsOfType<Character>())
        {
            if (character.CharacterTypes == Character.CharacterTypeEnum.Player)
            {
                playerRef = character;
                return;
            }
        }
    }

    protected void Update()
    {
        if (key.pickedUp && Vector3.Distance(transform.position, playerRef.transform.position) < distToDoorToOpen)
        {
            // open door
            // remove key from UI, remove door from scene
            UIManager.Instance.RemoveKey(key);
            Destroy(key.gameObject);
            Destroy(gameObject);
        }
    }
}