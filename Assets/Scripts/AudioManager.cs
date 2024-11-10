using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    
    public void Play(AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.Play();
    }

}
