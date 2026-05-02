using System.Collections;
using UnityEngine;

public class LevelGate : MonoBehaviour
{
    [Header("Win UI")]
    public GameObject winUI;

    [Header("No Key Feedback")]
    public TMPro.TextMeshProUGUI noKeyText;
    public AudioManager audioManager;
    public float noKeyTextDuration = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null && inventory.hasKey)
            {
                Debug.Log("Door opened!");

                inventory.hasKey = false;
                GetComponent<Collider2D>().enabled = false;

                PlayerMovement movement = other.GetComponent<PlayerMovement>();
                if (movement != null)
                {
                    movement.CanMove = false;
                    movement.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }

                if (winUI != null)
                    winUI.SetActive(true);

                if (audioManager != null)
                    audioManager.OnWin();
            }
            else
            {
                Debug.Log("You need a key to open this gate!");

                if (audioManager != null)
                    audioManager.OnNoKey();

                StartCoroutine(ShowNoKeyText());
            }
        }
    }

    IEnumerator ShowNoKeyText()
    {
        if (noKeyText != null)
        {
            noKeyText.gameObject.SetActive(true);
            yield return new WaitForSeconds(noKeyTextDuration);
            noKeyText.gameObject.SetActive(false);
        }
    }
}