using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

public class AudioManagerTest : MonoBehaviour {
    [Test]
    public void TestPlayingMusic() {
        GameObject audioManager = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Audio Manager.prefab"), 
                                  new Vector3(0, 0, 0),
                                  Quaternion.identity);

        // get the AudioManager component
        AudioManager aM = audioManager.GetComponent<AudioManager>();

        string currentMusic = aM.GetCurrentlyPlaying();

        // if the scene is the main menu, the music should be the main menu music
        if (SceneManager.GetActiveScene().name == "MainMenu") {
            Assert.AreEqual(currentMusic, "MainMenu");
        } else if (SceneManager.GetActiveScene().name == "TestScene" || 
                    SceneManager.GetActiveScene().name == "LevelOneScene" || 
                    SceneManager.GetActiveScene().name == "TutorialScene") {
            // if the character is in the bossDetect zone and the boss detect zone is not null
            BossDetect bossDetect = GameObject.FindObjectOfType<BossDetect>();
            if (bossDetect != null && bossDetect.isInBossZone) {
                Assert.AreEqual(currentMusic, "BossMusic");
            } else {
                Assert.AreEqual(currentMusic, "LevelOneMusic");
            }
        }
    }
}
