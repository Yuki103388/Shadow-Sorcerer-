using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioSource mainBGM, drums, other, clock;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [ContextMenu("Play")]
    public void Play()
    {
        mainBGM.Play();
    }

    [ContextMenu("Stage One")]
    public void StageOne()
    {
        StartCoroutine(FadeTrack(mainBGM, 4f, -1f, 0f));
        StartCoroutine(FadeTrack(clock, 4f, -1f, 1f));
    }

    //Fades track in or out. If startVolume is less than 0, it uses the current volume of the source.
    public IEnumerator FadeTrack(AudioSource source, float duration, float startVolume, float targetVolume)
    {
        float timeStart = mainBGM.time;
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
}
