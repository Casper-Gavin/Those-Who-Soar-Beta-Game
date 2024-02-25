using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {
    public static SoundManager instance;

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private enum SoundType {
        Walking,
        Running,
        Dash1,
        Dash2
    }

    protected override void Awake() {
        base.Awake();

        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void WalkingSound() {
        audioSource.clip = audioClips[(int)SoundType.Walking];

        audioSource.Play();

        audioSource.loop = true;
    }

    public void RunningSound() {
        audioSource.clip = audioClips[(int)SoundType.Running];

        audioSource.Play();

        audioSource.loop = true;
    }

    public void DashSound() {
        int randomDashSound = UnityEngine.Random.Range(1, 2);

        if (randomDashSound == 1) {
            audioSource.clip = audioClips[(int)SoundType.Dash1];
        } else {
            audioSource.clip = audioClips[(int)SoundType.Dash2];
        }

        audioSource.Play();

        audioSource.loop = false;
    }
}
