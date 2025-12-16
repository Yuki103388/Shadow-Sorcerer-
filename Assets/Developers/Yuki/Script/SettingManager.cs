using Oculus.Interaction;
using Oculus.Interaction.Locomotion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    public bool TunnelingEnabled = true;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Bootstrap()
    {
        if (Instance != null) return;

        var go = new GameObject("SettingsManager");
        go.AddComponent<SettingsManager>();
        DontDestroyOnLoad(go);
    }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += (_, __) => ApplyTunneling();
    }

    public void SetTunneling(bool enabled)
    {
        TunnelingEnabled = enabled;
        ApplyTunneling();
    }

    void ApplyTunneling()
    {
        var go = GameObject.Find("SmoothMovementTunneling");
        if (!go) return;

        var effect = go.GetComponent<TunnelingEffect>();
        if (effect) effect.enabled = TunnelingEnabled;

        var loco = go.GetComponent<LocomotionTunneling>();
        if (loco) loco.enabled = TunnelingEnabled;
    }
}
