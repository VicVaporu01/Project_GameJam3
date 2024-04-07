using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;

    private static AudioManager instance;

    public static AudioManager Instance
    {
        get => instance;
        private set => instance = value;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayAudioClip(int clipIndex)
    {
        if (clipIndex >= 0 && clipIndex < audioClips.Length)
        {
            audioSource.clip = audioClips[clipIndex];
            audioSource.Play();
        }
    }

    public void PlayRandomAudioClip()
    {
        int randomIndex = Random.Range(0, audioClips.Length);
        PlayAudioClip(randomIndex);
    }
    
    public AudioSource GetAudioSource()
    {
        return audioSource;
    }
    
}
