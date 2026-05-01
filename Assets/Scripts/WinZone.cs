using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public AudioManager audioManager;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.WinGame();
            audioManager.OnWin();
        }
    }
}
