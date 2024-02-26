using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager> {
    public Music[] music;
    public Sfx[] sfx;

    public static AudioManager instance;

    private readonly string AUDIO1KEY = "MainMenu";
    private readonly string AUDIO2KEY = "Gameplay";
    private readonly string AUDIO3KEY = "Boss";

    [SerializeField] private UIManager uiManager;
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

        foreach (Music m in music) {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
            m.source.volume = m.volume;
            m.source.pitch = m.pitch;
            m.source.loop = m.loop;
        }

        foreach (Sfx f in sfx) {
            f.source = gameObject.AddComponent<AudioSource>();
            f.source.clip = f.clip;
            f.source.volume = f.volume;
            f.source.pitch = f.pitch;
            f.source.loop = f.loop;
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
        if (uiManager == null) {
            uiManager = FindObjectOfType<UIManager>();
        }

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

        //Debug.Log(GetCurrentlyPlaying());

        /*
        if (uiManager != null) {
            if (UIManager.GameIsPaused) {
                PauseMusic(GetCurrentlyPlayingMusic());

                PauseSFX(GetCurrentlyPlayingSFX());
                return;
            } else {
                UnPauseMusic(GetCurrentlyPlayingMusic());

                UnPauseSFX(GetCurrentlyPlayingSFX());
            }
        }
        */
    }   

    public void Play(string name) {
        Music m = Array.Find(music, music => music.name == name);
        if (m == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        m.source.Play();
    }

    public void PlaySFX(string name) {
        Sfx f = Array.Find(sfx, sfx => sfx.name == name);
        if (f == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        f.source.Play();
    }

    public void Stop(string name) {
        Music m = Array.Find(music, music => music.name == name);

        m.source.Stop();
    }

    public void StopSFX(string name) {
        Sfx f = Array.Find(sfx, sfx => sfx.name == name);

        f.source.Stop();
    }

    public void Pause(string name) {
        Music m = Array.Find(music, music => music.name == name);

        m.source.Pause();
    }

    public void PauseSFX(string name) {
        Sfx f = Array.Find(sfx, sfx => sfx.name == name);

        f.source.Pause();
    }

    public void UnPause(string name) {
        Music m = Array.Find(music, music => music.name == name);

        m.source.UnPause();
    }

    public void UnPauseSFX(string name) {
        Sfx f = Array.Find(sfx, sfx => sfx.name == name);

        f.source.UnPause();
    }

    public void SetVolume(string name, float volume) {
        Music m = Array.Find(music, music => music.name == name);

        m.source.volume = volume;

        if (name == "MainMenu") {
            PlayerPrefs.SetFloat(AUDIO1KEY, volume);
        } else if (name == "Gameplay") {
            PlayerPrefs.SetFloat(AUDIO2KEY, volume);
        } else if (name == "Boss") {
            PlayerPrefs.SetFloat(AUDIO3KEY, volume);
        }
    }

    public float GetVolume(string name) {
        Music m = Array.Find(music, music => music.name == name);
        
        if (name == "MainMenu") {
            m.source.volume = PlayerPrefs.GetFloat(AUDIO1KEY);
        } else if (name == "Gameplay") {
            m.source.volume = PlayerPrefs.GetFloat(AUDIO2KEY);
        } else if (name == "Boss") {
            m.source.volume = PlayerPrefs.GetFloat(AUDIO3KEY);
        }

        return m.source.volume;
    }

    public string GetCurrentlyPlaying() {
        foreach (Music m in music) {
            if (m.source.isPlaying) {
                return m.name;
            }
        }

        return null;
    }

    public string GetCurrentlyPlayingSFX() {
        foreach (Sfx f in sfx) {
            if (f.source.isPlaying) {
                return f.name;
            }
        }

        return null;
    }

    public string GetCurrentlyPlayingTag() {
        foreach (Music m in music) {
            if (m.source.isPlaying) {
                return m.tag;
            }
        }

        return null;
    }

    public string GetCurrentlyPlayingSFXTag() {
        foreach (Sfx f in sfx) {
            if (f.source.isPlaying) {
                return f.tag;
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