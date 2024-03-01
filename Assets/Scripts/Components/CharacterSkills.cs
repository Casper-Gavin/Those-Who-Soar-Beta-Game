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

    public void Update() {
        UpdateUI();
    }

    public void UpdateUI() {
        TitleText.text = $"{skillMenu.skillLevels[id]}/{skillMenu.skillCaps[id]}\n{skillMenu.skillNames[id]}\nCost: {skillMenu.skillPoints}/{skillMenu.skillCosts[id]} SP";

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
