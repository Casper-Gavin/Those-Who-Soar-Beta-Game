using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;

    public static AudioManager instance;

    public void Awake() {
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
        // Later we will add (here or maybe in update) a check to see if the current scene is the main menu, and if so, play the main menu music.
        Play("MainMenu");
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

    public void SetVolume(string name, float volume) {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.volume = volume;
    }

    public float GetVolume(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);

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
}
