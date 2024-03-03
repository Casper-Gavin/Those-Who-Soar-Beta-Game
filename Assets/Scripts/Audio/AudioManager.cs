using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : Singleton<AudioManager> {
    public Music[] music;
    public Sfx[] sfx;

    private readonly string MUSICKEY = "MUSIC_KEY";
    //private readonly string AUDIO1KEY = "MainMenu";
    //private readonly string AUDIO2KEY = "Gameplay";
    //private readonly string AUDIO3KEY = "Boss";

    [SerializeField] private UIManager uiManager;
    [SerializeField] private BossDetect bossDetect;

    protected override void Awake() {
        base.Awake();

        // VOLUME THING - make stuff static?
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
        } else {
            Play("Gameplay");
        }
    }

    private void Update() {
        if (uiManager == null) {
            uiManager = FindObjectOfType<UIManager>();
        }

        if (bossDetect == null) {
            bossDetect = FindObjectOfType<BossDetect>();
        }

        if (SceneManager.GetActiveScene().name == "MainMenu" && GetCurrentlyPlayingTag() != "MenuMusic") {
            Stop("Gameplay");

            Play("MainMenu");
        } else if (SceneManager.GetActiveScene().name != "MainMenu" && GetCurrentlyPlayingTag() != "GameMusic" && (!bossDetect || !bossDetect.isInBossZone)) {
            Stop("MainMenu");
            Stop("Boss");

            Play("Gameplay");
        } else if (bossDetect != null) {
            if (GetCurrentlyPlayingTag() != "BossMusic" && bossDetect.isInBossZone && !bossDetect.isBossDead) {
                Stop("Gameplay");

                Play("Boss");
            }
        } else if (SceneManager.GetActiveScene().name != "MainMenu" && GetCurrentlyPlaying() == "Boss") {
            if (bossDetect.isBossDead || bossDetect.isInBossZone == false) {
                Stop("Boss");
                Play("Gameplay");
            }
        }

    }   

    public void Play(string name) {
        Music m = Array.Find(music, music => music.name == name);
        if (m == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        m.source.Play();
        // in case we want some music to have a different volume controller
        if (name == "MainMenu" || name == "Gameplay" || name == "Boss")
        {
            m.source.volume = PlayerPrefs.GetFloat(MUSICKEY);
        }
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

        // in case we want some music to have a different volume controller
        if (name == "MainMenu" || name == "Gameplay" || name == "Boss")
        {
            PlayerPrefs.SetFloat(MUSICKEY, volume);
            Debug.Log("MUSICKEY set to " + volume);
        }
    }

    public void SetSFXVolume(string name, float volume) {
        Sfx f = Array.Find(sfx, sfx => sfx.name == name);

        f.source.volume = volume;
    }

    public float GetVolume(string name) {
        Music m = Array.Find(music, music => music.name == name);
        
        if (name == "MainMenu" || name == "Gameplay" || name == "Boss")
        {
            m.source.volume = PlayerPrefs.GetFloat(MUSICKEY);
        }

        return m.source.volume;
    }

    public float GetSFXVolume(string name) {
        Sfx f = Array.Find(sfx, sfx => sfx.name == name);

        return f.source.volume;
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

    public bool IsPlaying(string name) {
        Music m = Array.Find(music, music => music.name == name);

        return m.source.isPlaying;
    }

    public bool IsPlayingSFX(string name) {
        Sfx f = Array.Find(sfx, sfx => sfx.name == name);

        return f.source.isPlaying;
    }

    public void FadeOut(string name) {
        Music m = Array.Find(music, music => music.name == name);

        StartCoroutine(FadeOutMusic(m.source, 1f));
    }

    public void FadeOutSFX(string name) {
        Sfx f = Array.Find(sfx, sfx => sfx.name == name);

        StartCoroutine(FadeOutSFX(f.source, 1f));
    }

    private IEnumerator FadeOutMusic(AudioSource audioSource, float FadeTime) {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    private IEnumerator FadeOutSFX(AudioSource audioSource, float FadeTime) {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public static void ClickButton() {
        Instance.PlaySFX("ClickButton");
    }

    public static void ClickSkill() {
        Instance.PlaySFX("ClickSkill");
    }
}