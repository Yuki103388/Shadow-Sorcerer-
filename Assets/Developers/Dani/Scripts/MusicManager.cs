using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FadeTrackCall
{
    [Tooltip("Set to audiosources index")] public int source;
    [Range(0, 5)] public float duration;
    [Range(0, 2)] public float targetVolume;
}

[System.Serializable]
public class ChangeSpeedCall
{
    [Range(0, 5)] public float duration;
    [Range(0, 3)][Tooltip("0 is stopped, 2 is double speed. Also affects pitch")] public float newSpeed;
}

[System.Serializable]
public class MusicStage
{
    public int timeStart;
    public FadeTrackCall[] fadeCalls = new FadeTrackCall[0];
    public ChangeSpeedCall[] speedCalls = new ChangeSpeedCall[0];
}

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [Tooltip("First index must be the main bgm")] public AudioSource[] audioSources;
    public int currentStage;
    [SerializeField] public MusicStage[] stages;

    [TextArea(8, 1000)]
    public string developerNotes = "All stages need to be in order from first to last. \n The timeStart is the time remaining in seconds when the stage should start. \n Example: If you want a stage to start at 2 minutes remaining, set timeStart to 120.";

    private void Awake()
    {
        instance = this;
    }

    [ContextMenu("Play")]
    public void Play()
    {
        audioSources[0].Play();
    }

    public void NextStage()
    {
        Debug.Log("Activating music stage " + currentStage);
        MusicStage stage = stages[currentStage];
        foreach (var fadeCall in stage.fadeCalls)
        {
            StartCoroutine(FadeTrack(audioSources[fadeCall.source], fadeCall.duration, -1f, fadeCall.targetVolume));
        }
        foreach (var speedCall in stage.speedCalls)
        {
            StartCoroutine(ChangeSpeed(speedCall.duration, speedCall.newSpeed));
        }
        currentStage++;
    }

    //Fades track in or out. If startVolume is less than 0, it uses the current volume of the source.
    public IEnumerator FadeTrack(AudioSource source, float duration, float startVolume, float targetVolume)
    {
        float timeStart = audioSources[0].time;
        if (startVolume < 0f) startVolume = source.volume;
        source.Play();
        source.time = timeStart;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / duration);
            yield return null;
        }

        source.volume = targetVolume;
    }

    public IEnumerator ChangeSpeed(float duration, float newSpeed)
    {
        float currentSpeed = audioSources[0].pitch;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float speed = Mathf.Lerp(currentSpeed, newSpeed, elapsed / duration);
            for (int i = 0; i < audioSources.Length; i++)
            {
                audioSources[i].pitch = speed;
            }
            yield return null;
        }

        for (int i = 0; i < audioSources.Length; i++)
            audioSources[i].pitch = newSpeed;
    }
}
