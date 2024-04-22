using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPointManager : Singleton<SkillPointManager> {
    public int SkillPoints { get; set; }
    public int SkillPointsTotal { get; set; }

    private readonly string SKILLPOINTS_KEY = "MyGame_MySkillPoints_DontCheat";
    private readonly string SKILLPOINTSTOTAL_KEY = "MyGame_MySkillPointsTotal_DontCheat";

    [SerializeField] private GameObject skillPointUI;

    private void Start() {
        skillPointUI.SetActive(true);
        LoadSkillPoints();
        LoadSkillPointsTotal();
        skillPointUI.SetActive(false);
    }

    private void OnApplicationQuit() {
        SaveSkillPoints();
        SaveSkillPointsTotal();
    }

    private void LoadSkillPoints() {
        SkillPoints = PlayerPrefs.GetInt(SKILLPOINTS_KEY);
    }

    private void LoadSkillPointsTotal() {
        SkillPointsTotal = PlayerPrefs.GetInt(SKILLPOINTSTOTAL_KEY);
    }

    public void AddSkillPoints(int amount) {
        SkillPoints += amount;
        PlayerPrefs.SetInt(SKILLPOINTS_KEY, SkillPoints);
        //UIManager.Instance.FlashCoinEffect();
    }

    public void SaveSkillPoints() {
        PlayerPrefs.SetInt(SKILLPOINTS_KEY, SkillPoints);
    }

    public void SaveSkillPointsTotal() {
        PlayerPrefs.SetInt(SKILLPOINTSTOTAL_KEY, SkillPointsTotal);
    }

    public void RemoveSkillPoints(int amount) {
        SkillPoints -= amount;
        PlayerPrefs.SetInt(SKILLPOINTS_KEY, SkillPoints);
    }

    public void RemoveSkillPointsTotal(int amount) {
        SkillPointsTotal -= amount;
        PlayerPrefs.SetInt(SKILLPOINTSTOTAL_KEY, SkillPointsTotal);
    }

    public void ResetSkillPoints() {
        SkillPoints = 0;
        PlayerPrefs.SetInt(SKILLPOINTS_KEY, SkillPoints);
    }

    public void ResetSkillPointsTotal() {
        SkillPointsTotal = 0;
        PlayerPrefs.SetInt(SKILLPOINTSTOTAL_KEY, SkillPointsTotal);
    }
}