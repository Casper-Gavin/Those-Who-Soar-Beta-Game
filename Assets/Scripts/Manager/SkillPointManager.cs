using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPointManager : Singleton<SkillPointManager> {
    public int SkillPoints { get; set; }
    public int SkillPointsTotal { get; set; }

    private readonly string SKILLPOINTS_KEY = "MyGame_MySkillPoints_DontCheat";
    private readonly string SKILLPOINTSTOTAL_KEY = "MyGame_MySkillPointsTotal_DontCheat";

    private void Start() {
        LoadSkillPoints();
    }

    private void OnApplicationQuit() {
        SaveSkillPoints();
    }

    private void LoadSkillPoints() {
        SkillPoints = PlayerPrefs.GetInt(SKILLPOINTS_KEY);
    }

    public void AddSkillPoints(int amount) {
        SkillPoints += amount;
        PlayerPrefs.SetInt(SKILLPOINTS_KEY, SkillPoints);
        //UIManager.Instance.FlashCoinEffect();
    }

    public void SaveSkillPoints() {
        PlayerPrefs.SetInt(SKILLPOINTS_KEY, SkillPoints);
        PlayerPrefs.SetInt(SKILLPOINTSTOTAL_KEY, SkillPointsTotal);
    }

    public void RemoveSkillPoints(int amount) {
        SkillPoints -= amount;
        PlayerPrefs.SetInt(SKILLPOINTS_KEY, SkillPoints);
    }

    public void ResetSkillPoints() {
        SkillPoints = 0;
        PlayerPrefs.SetInt(SKILLPOINTS_KEY, SkillPoints);
    }
}