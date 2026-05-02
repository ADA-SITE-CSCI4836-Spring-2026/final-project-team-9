using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelGate : MonoBehaviour
{
    public CinemachineVirtualCamera nextLevelCam;
    public CinemachineVirtualCamera previousLevelCam;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null && inventory.hasKey)
            {
                Debug.Log("Door opened! Moving to next level.");

                inventory.hasKey = false;

                nextLevelCam.Priority = 10;
                previousLevelCam.Priority = 0;

                GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                Debug.Log("You need a key to open this gate!");
            }
        }
    }
}
