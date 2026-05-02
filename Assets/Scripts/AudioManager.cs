using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("The Speaker")]
    public AudioSource sfxSource;

    [Header("The Sound Files")]
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip winSound;
    public AudioClip loseSound;

    public void OnJump()
    {
        // PlayOneShot plays the clip once. The "null" checks prevent the game from crashing if you forget to assign a sound!
        if (jumpSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(jumpSound);
        }
    }

    public void OnLand()
    {
        if (landSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(landSound);
        }
    }

    public void OnWin()
    {
        if (winSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(winSound);
        }
    }

    public void OnLose()
    {
        if (loseSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(loseSound);
        }
    }
}
