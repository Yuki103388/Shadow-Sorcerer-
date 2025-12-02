using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("References")]
    [SerializeField] private MusicManager musicManager;
    [Header("Settings")]
    [SerializeField] private float timeRemaining;


    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        Timer();
    }

    //put in timer script later
    public void Timer()
    {
        if (timeRemaining > 0)
            timeRemaining -= Time.deltaTime;
        else
            timeRemaining = 0;

        if (musicManager.currentStage < musicManager.stages.Length && timeRemaining <= musicManager.stages[musicManager.currentStage].timeStart)
        {
            musicManager.NextStage();
        }
    }

    private void CheckForMusicStage()
    {
        if (musicManager != null)
        {
            if (musicManager.currentStage < musicManager.stages.Length &&
                timeRemaining >= musicManager.stages[musicManager.currentStage].timeStart)
            {
                musicManager.NextStage();
            }
        }
    }
}
