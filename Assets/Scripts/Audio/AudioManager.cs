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
        if (GameObject.FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        base.Awake();

        DontDestroyOnLoad(gameObject);

        foreach (Music m in music) {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
            m.source.volume = PlayerPrefs.GetFloat(MUSICKEY);
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
            PlaySFX("Wind");
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
            StopAllMusic();
            Play("MainMenu");
            if (GetCurrentlyPlayingSFX() != "Wind") {
                PlaySFX("Wind");
            }
        } else if (SceneManager.GetActiveScene().name != "MainMenu" && GetCurrentlyPlayingTag() != "GameMusic" && (!bossDetect || !bossDetect.isInBossZone)) {
            StopAllMusic();
            StopSFX("Wind");
            Play("Gameplay");
        } else if (bossDetect != null) {
            if (GetCurrentlyPlayingTag() != "BossMusic" && bossDetect.isInBossZone && !bossDetect.isBossDead) {
                StopAllMusic();
                StopSFX("Wind");
                Play("Boss");
            }
        } else if (SceneManager.GetActiveScene().name != "MainMenu" && GetCurrentlyPlaying() == "Boss") {
            if (bossDetect.isBossDead || bossDetect.isInBossZone == false) {
                StopAllMusic();
                StopSFX("Wind");
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
    }

    public void PlaySFX(string name) {
        Sfx f = Array.Find(sfx, sfx => sfx.name == name);
        if (f == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        f.source.Play();
    }

    /*
    public Sfx[] MakeCopyOfSameSFX(string name, int count) {
        Sfx f = Array.Find(sfx, sfx => sfx.name == name);
        if (f == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return null;
        }

        Sfx[] sfxArray = new Sfx[count];
        for (int i = 0; i < count; i++) {
            sfxArray[i] = new Sfx();
            sfxArray[i].source = gameObject.AddComponent<AudioSource>();
            sfxArray[i].source.name = name + i;
            sfxArray[i].source.clip = f.clip;
            sfxArray[i].source.volume = f.volume;
            sfxArray[i].source.pitch = f.pitch;
            sfxArray[i].source.loop = f.loop;
        }

        return sfxArray;
    }
    */

    public void MakeAndPlaySFX(string name) {
        // play the first available sfx of the same name
        Sfx[] allSfx = Array.FindAll(sfx, sfx => sfx.name == name);
        foreach (Sfx f in allSfx) {
            if (!f.source.isPlaying) {
                f.source.Play();
                return;
            }
        }
    }

    public void MakeAndPlaySFXVariable(string name, float pitch, float volume) {
        // play the first available sfx of the same name
        Sfx[] allSfx = Array.FindAll(sfx, sfx => sfx.name == name);
        foreach (Sfx f in allSfx) {
            if (!f.source.isPlaying) {
                f.source.pitch = pitch;
                f.source.volume = volume;
                f.source.Play();
                return;
            }
        }
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

    public void StopAllMusic()
    {
        foreach (Music m in music)
        {
            m.source.Stop();
        }
    }

    public void SetVolume(float volume) {
        PlayerPrefs.SetFloat(MUSICKEY, volume);
        foreach (Music m in music)
        {
            m.source.volume = volume;
        }
    }

    public void SetSFXVolume(string name, float volume) {
        Sfx f = Array.Find(sfx, sfx => sfx.name == name);

        f.source.volume = volume;
    }

    public float GetVolume() {
        return PlayerPrefs.GetFloat(MUSICKEY);
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