using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillMenu : MonoBehaviour {
    public static SkillMenu skillMenu;

    private void Awake() => skillMenu = this; // Singleton

    public int[] skillLevels;
    public int[] skillCosts;
    public int[] skillCaps;
    public string[] skillNames;

    // Enum for each skill
    public enum SkillEnum {
        IncreaseDamage , // 0
        IncreaseHealth, // 1
        IncreaseShield, // 2
        IncreaseSpeed, // 3
        IncreaseDash // 4
    }

    public List<CharacterSkills> skillList;
    public List<GameObject> connectorList;
    public GameObject SkillHolder;
    public GameObject ConnectorHolder;

    public int skillPoints;

    private void Start() {
        skillPoints = 20; // For testing only

        skillLevels = new int[5];
        skillCaps = new[] { 5, 3, 3, 1, 1 };
        skillCosts = new[] { 1, 2, 2, 3, 3 };

        skillNames = new[] { "+1 Damage", "+2 Health", "+1 Shield", "Inc. Speed", "Inc. Dash" };

        foreach (var skill in SkillHolder.GetComponentsInChildren<CharacterSkills>()) {
            skillList.Add(skill);
        }

        /* Can be used to connect skills later if we want, still need to implement this
        // https://www.youtube.com/watch?v=fE0R6WLpmrE
        foreach (var connector in ConnectorHolder.GetComponentsInChildren<RectTransform>()) {
            connectorList.Add(connector.gameObject);
        }
        */

        for (var i = 0; i < skillList.Count; i++) {
            skillList[i].id = i;
        }

        UpdateAllSkillUI();
    }

    public void UpdateAllSkillUI() {
        foreach (var skill in skillList) {
            skill.UpdateUI();
        }
    }
}
