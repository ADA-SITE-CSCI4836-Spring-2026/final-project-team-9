using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    public AudioManager audioManager;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.LoseGame();
            audioManager.OnLose();
        }
    }
}
