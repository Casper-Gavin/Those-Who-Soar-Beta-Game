using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillTreeMenu : MonoBehaviour {
    [SerializeField] private HealthBase healthBase;
    [SerializeField] private PlayerHealth playerHealth;
    private void Start() {
        healthBase = FindObjectOfType<HealthBase>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    public void OnClickPlusOneDmg() {
        Debug.Log("PlusOneDmg");
    }

    public void OnClickPlusOneHealth() {
        Debug.Log("PlusOneHealth");
    }

    public void OnClickPlusOneShield() {
        Debug.Log("PlusOneShield");
    }
}
