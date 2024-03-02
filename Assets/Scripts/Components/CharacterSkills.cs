using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static SkillMenu;

public class CharacterSkills : MonoBehaviour {
    public int id;
    
    public TMP_Text TitleText;

    public int[] ConnectedSkills;

    [SerializeField] private GameObject skillTreeMenu;

    private void Start() {
        skillTreeMenu = GameObject.Find("SkillTreeMenu");
    }

    public void Update() {
        if (skillTreeMenu == null) {
            skillTreeMenu = GameObject.Find("SkillTreeMenu");
        } else {
            if (skillTreeMenu.activeSelf) {
                UpdateAllUI();
            }
        }
    }

    public void UpdateAllUI() {
        for (var i = 0; i < skillMenu.skillList.Count; i++) {
            skillMenu.skillList[i].UpdateUI(i);
        }
    }

    public void UpdateUI(int id) {
        TitleText.text = skillMenu.skillNames[id] + " " + skillMenu.skillLevels[id] + "/" + skillMenu.skillCaps[id];

        GetComponent<Image>().color = skillMenu.skillLevels[id] >= skillMenu.skillCaps[id] ? Color.yellow : skillMenu.skillPoints >= skillMenu.skillCosts[id] ? Color.green : Color.red;
    }

    public void Buy() {
        if (skillMenu.skillPoints < skillMenu.skillCosts[id] || skillMenu.skillLevels[id] >= skillMenu.skillCaps[id]) return;
        skillMenu.skillPoints -= skillMenu.skillCosts[id];
        skillMenu.skillLevels[id] ++;

        SkillPointManager.Instance.RemoveSkillPointsTotal(skillMenu.skillCosts[id]);

        skillMenu.UpdateAllSkillUI();
    }
}
