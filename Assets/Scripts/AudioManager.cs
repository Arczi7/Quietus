using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("Audio Settings")]
    [SerializeField] private AudioSource backgroundMusic;
    [Range(0f, 1f)]
    [SerializeField] private float musicVolume = 0.1f;
    [SerializeField] private AudioSource effectsSound;
    [Range(0f, 1f)]
    [SerializeField] private float effectsVolume = 0.4f;
    [SerializeField] private AudioSource playerEffectsSound;
    [Range(0f, 1f)]
    [SerializeField] private float playerEffectsVolume = 0.4f;
    [Header("Music and Effects")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private List<AudioClip> gameBackgroundMusicList;
    private int backgroundMusicIndex = 0;
    
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateVolumes();
    }

    private void UpdateVolumes()
    {
        backgroundMusic.volume = musicVolume;
        effectsSound.volume = effectsVolume;
        playerEffectsSound.volume = playerEffectsVolume;
    }

    private void PlayMenuMusic()
    {
        backgroundMusic.clip = menuMusic;
        backgroundMusic.loop = true;
        backgroundMusic.Play();
    }

    private IEnumerator PlayMusicInGame()
    {
        AudioClip currentClip = gameBackgroundMusicList[backgroundMusicIndex];
        backgroundMusic.clip = currentClip;
        backgroundMusic.Play();
        yield return new WaitForSeconds(currentClip.length);
        backgroundMusicIndex += 1;
        if(backgroundMusicIndex > gameBackgroundMusicList.Count)
        {
            backgroundMusicIndex = 0;
        }
    }

    public void BackgroundMusicManager(string sceneName)
    {
        if(sceneName == "SceneMenu")
        {
            PlayMenuMusic();
        }
        else if(sceneName == "Scene01")
        {
            StartCoroutine(PlayMusicInGame());
        }
    }

    public void PlayEffect(AudioClip effect)
    {
        effectsSound.clip = effect;
        effectsSound.Play();
    }

    public void PlayPlayerEffect(AudioClip effect)
    {
        playerEffectsSound.clip = effect;
        playerEffectsSound.Play();        
    }

    public void StopMusic()
    {
        backgroundMusic.Stop();
    }

}
