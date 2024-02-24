using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager> {
    public Sound[] sounds;

    public static AudioManager instance;

    private readonly string AUDIO1KEY = "Audio1"; // MainMenu
    private readonly string AUDIO2KEY = "Audio2"; // Gameplay
    private readonly string AUDIO3KEY = "Audio3"; // Boss

    [SerializeField] private BossDetect bossDetect;

    protected override void Awake() {
        base.Awake();

        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start() {
        if (SceneManager.GetActiveScene().name == "MainMenu") {
            Play("MainMenu");
            SetVolume("MainMenu", PlayerPrefs.GetFloat(AUDIO1KEY));
        } else {
            Play("Gameplay");
            SetVolume("Gameplay", PlayerPrefs.GetFloat(AUDIO2KEY));
        }
    }

    /*
    private void OnEnable() {
        bossDetect.OnPlayerEnterBossZone.AddListener(PlayBossMusic);
        bossDetect.OnPlayerExitBossZone.AddListener(PlayGameMusic);
        bossDetect.OnBossDead.AddListener(StopBossMusic);
    }

    private void OnDisable() {
        bossDetect.OnPlayerEnterBossZone.RemoveListener(PlayBossMusic);
        bossDetect.OnPlayerExitBossZone.RemoveListener(PlayGameMusic);
        bossDetect.OnBossDead.RemoveListener(StopBossMusic);
    }
    */

    private void Update() {
        if (bossDetect == null) {
            bossDetect = FindObjectOfType<BossDetect>();
        }

        /*
        // Set up the listeners for the boss music
        if (bossDetect.OnPlayerEnterBossZone == null) {
            bossDetect.OnPlayerEnterBossZone.AddListener(PlayBossMusic);
        }

        if (bossDetect.OnPlayerExitBossZone == null) {
            bossDetect.OnPlayerExitBossZone.AddListener(PlayGameMusic);
        }

        if (bossDetect.OnBossDead == null) {
            bossDetect.OnBossDead.AddListener(StopBossMusic);
        }
        */

        if (SceneManager.GetActiveScene().name == "MainMenu" && GetCurrentlyPlayingTag() != "MenuMusic") {
            Stop("Gameplay");

            Play("MainMenu");
            SetVolume("MainMenu", PlayerPrefs.GetFloat(AUDIO1KEY));
        } else if (SceneManager.GetActiveScene().name != "MainMenu" && GetCurrentlyPlayingTag() != "GameMusic" && !bossDetect.isInBossZone) {
            Stop("MainMenu");
            Stop("Boss");

            Play("Gameplay");
            SetVolume("Gameplay", PlayerPrefs.GetFloat(AUDIO2KEY));
        } else if (bossDetect != null) {
            if (GetCurrentlyPlayingTag() != "BossMusic" && bossDetect.isInBossZone && !bossDetect.isBossDead) {
                Stop("Gameplay");

                Play("Boss");
                SetVolume("Boss", PlayerPrefs.GetFloat(AUDIO2KEY));
            }
        } else if (SceneManager.GetActiveScene().name != "MainMenu" && GetCurrentlyPlaying() == "Boss") {
            if (bossDetect.isBossDead || bossDetect.isInBossZone == false) {
                Stop("Boss");
                Play("Gameplay");
                SetVolume("Gameplay", PlayerPrefs.GetFloat(AUDIO2KEY));
            }
        }
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (UIManager.GameIsPaused) {
            s.source.Pause();
            return;
        } else {
            s.source.UnPause();
        }

        s.source.Play();
    }

    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.Stop();
    }

    public void SetVolume(string name, float volume) {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.volume = volume;

        if (name == "MainMenu") {
            PlayerPrefs.SetFloat(AUDIO1KEY, volume);
        } else if (name == "Gameplay") {
            PlayerPrefs.SetFloat(AUDIO2KEY, volume);
        } else if (name == "Boss") {
            PlayerPrefs.SetFloat(AUDIO3KEY, volume);
        }
    }

    public float GetVolume(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (name == "MainMenu") {
            s.source.volume = PlayerPrefs.GetFloat(AUDIO1KEY);
        } else if (name == "Gameplay") {
            s.source.volume = PlayerPrefs.GetFloat(AUDIO2KEY);
        } else if (name == "Boss") {
            s.source.volume = PlayerPrefs.GetFloat(AUDIO3KEY);
        }

        return s.source.volume;
    }

    public string GetCurrentlyPlaying() {
        foreach (Sound s in sounds) {
            if (s.source.isPlaying) {
                return s.name;
            }
        }

        return null;
    }

    public string GetCurrentlyPlayingTag() {
        foreach (Sound s in sounds) {
            if (s.source.isPlaying) {
                return s.tag;
            }
        }

        return null;
    }

    public void PlayBossMusic() {
        Stop("Gameplay");

        Play("Boss");
        SetVolume("Boss", PlayerPrefs.GetFloat(AUDIO3KEY));
    }

    public void PlayGameMusic() {
        Stop("Boss");

        Play("Gameplay");
        SetVolume("Gameplay", PlayerPrefs.GetFloat(AUDIO2KEY));
    }

    public void StopBossMusic() {
        Stop("Boss");

        Play("Gameplay");
        SetVolume("Gameplay", PlayerPrefs.GetFloat(AUDIO2KEY));
    }
}