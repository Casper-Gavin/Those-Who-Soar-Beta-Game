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

    private void Awake() {
        StartCoroutine(LoadSkillLevelsInitial());
    }

    private void OnApplicationQuit() {
        SaveSkillLevels();
    }

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
        LoadSkillLevels();

        for (var i = 0; i < skillMenu.skillList.Count; i++) {
            skillMenu.skillList[i].UpdateUI(i);
        }
    }

    public void UpdateUI(int id) {
        TitleText.text = $"{skillMenu.skillLevels[id]}/{skillMenu.skillCaps[id]}\n{skillMenu.skillNames[id]}\nCost: {skillMenu.skillPoints}/{skillMenu.skillCosts[id]} SP";

        GetComponent<Image>().color = skillMenu.skillLevels[id] >= skillMenu.skillCaps[id] ? Color.yellow : skillMenu.skillPoints >= skillMenu.skillCosts[id] ? Color.green : Color.red;
    }

    public void Buy() {
        if (skillMenu.skillPoints < skillMenu.skillCosts[id] || skillMenu.skillLevels[id] >= skillMenu.skillCaps[id]) return;
        skillMenu.skillPoints -= skillMenu.skillCosts[id];
        skillMenu.skillLevels[id] ++;

        SkillPointManager.Instance.RemoveSkillPointsTotal(skillMenu.skillCosts[id]);

        SaveSkillLevels();

        skillMenu.UpdateAllSkillUI();
    }

    public void SaveSkillLevels() {
        for (var i = 0; i < skillMenu.skillList.Count; i++) {
            PlayerPrefs.SetInt($"SkillLevel_{i}", skillMenu.skillLevels[i]);

            if (!GameManager.Instance.PLAYER_PREF_KEYS.Contains($"SkillLevel_{i}")) {
                GameManager.Instance.PLAYER_PREF_KEYS.Add($"SkillLevel_{i}");
            }
        }
    }

    public IEnumerator LoadSkillLevelsInitial()
    {
        yield return null; // wait for next frame, skillMenu may not be initialized yet
        LoadSkillLevels();
    }

    public void LoadSkillLevels()
    {
        for (var i = 0; i < skillMenu.skillList.Count; i++) {
            skillMenu.skillLevels[i] = PlayerPrefs.GetInt($"SkillLevel_{i}");

            if (!GameManager.Instance.PLAYER_PREF_KEYS.Contains($"SkillLevel_{i}")) {
                GameManager.Instance.PLAYER_PREF_KEYS.Add($"SkillLevel_{i}");
            }
        }
    }
}
