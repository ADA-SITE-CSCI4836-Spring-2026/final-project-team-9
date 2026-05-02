using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("The Speaker")]
    public AudioSource sfxSource;

    [Header("Background Music")]
    public AudioSource musicSource;
    public AudioClip backgroundMusic;
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;

    [Header("The Sound Files")]
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip noKeySound;

    void Start()
    {
        if (backgroundMusic != null && musicSource != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.volume = musicVolume;
            musicSource.Play();
        }
    }

    public void OnJump()
    {
        if (jumpSound != null && sfxSource != null)
            sfxSource.PlayOneShot(jumpSound);
    }

    public void OnLand()
    {
        if (landSound != null && sfxSource != null)
            sfxSource.PlayOneShot(landSound);
    }

    public void OnWin()
    {
        if (winSound != null && sfxSource != null)
        {
            // stop background music on win
            if (musicSource != null) musicSource.Stop();
            sfxSource.PlayOneShot(winSound);
        }
    }

    public void OnLose()
    {
        if (loseSound != null && sfxSource != null)
        {
            // stop background music on lose
            if (musicSource != null) musicSource.Stop();
            sfxSource.PlayOneShot(loseSound);
        }
    }

    public void OnNoKey()
    {
        if (noKeySound != null && sfxSource != null)
            sfxSource.PlayOneShot(noKeySound);
    }
}