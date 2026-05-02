using UnityEngine;

public class TimeHealth : MonoBehaviour
{
    public float maxTime = 10f;
    public float currentTime;

    public float drainMoving = 1f;
    public float drainIdle = 1.4f;

    public enum PlayerState { Idle, Moving }
    public PlayerState currentState;

    void Start()
    {
        currentTime = maxTime;
    }

    void Update()
    {
        float drainRate = (currentState == PlayerState.Moving) ? drainMoving : drainIdle;

        currentTime -= drainRate * Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            GameManager.Instance.LoseGame();
        }
    }

    public float GetNormalized()
    {
        return currentTime / maxTime;
    }
}