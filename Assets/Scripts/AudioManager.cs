using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip MenuMusic;
    public AudioClip GameMusic;
    public AudioClip Buttons;

    private bool isPlayingMenuMusic;

    public static AudioManager instance;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Start()
    {
        musicSource.clip = MenuMusic;
        musicSource.Play();
        isPlayingMenuMusic = true;
    }

    private void Update()
    {

    }

    public void PlayGameMusic()
    {
        //StopAllCoroutines();
        if (isPlayingMenuMusic)
        {
            StartCoroutine(FadeMusic(MenuMusic, GameMusic, 1));
            isPlayingMenuMusic = !isPlayingMenuMusic;
        }
        else
        {
            StartCoroutine(FadeMusic(GameMusic, MenuMusic, 1));
            isPlayingMenuMusic = !isPlayingMenuMusic;
        }
        
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    private IEnumerator FadeMusic(AudioClip musicToStop, AudioClip musicToStart, float maxVolume)
    {
        float timeToFadeOut = 2f;
        float timeToFadeIn = 2.1f;
        float timeElapsed = 0;
        musicSource.clip = musicToStop;

        //fade out
        while (timeElapsed < timeToFadeOut)
        {
            musicSource.volume = Mathf.Lerp(maxVolume, 0, timeElapsed / timeToFadeOut);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        musicSource.Stop();
        
        yield return new WaitForSeconds(1);

        //fade in
        musicSource.clip = musicToStart;
        musicSource.Play();
        while (timeElapsed < timeToFadeIn)
        {
            musicSource.volume = Mathf.Lerp(0, maxVolume, timeElapsed / timeToFadeIn);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

}

