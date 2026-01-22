using Oculus.Interaction;
using Oculus.Interaction.Locomotion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    // --- Keys (opslag) ---
    private const string KEY_TUNNELING = "TunnelingEnabled";
    private const string KEY_MOVE_SPEED = "MoveSpeed"; // bv. 30

    // --- State ---
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

        // laad opgeslagen settings
        TunnelingEnabled = PlayerPrefs.GetInt(KEY_TUNNELING, 1) == 1;

        SceneManager.sceneLoaded += (_, __) => ApplyAll();
    }

    // TUNNELING 
    public void SetTunneling(bool enabled)
    {
        TunnelingEnabled = enabled;
        PlayerPrefs.SetInt(KEY_TUNNELING, enabled ? 1 : 0);
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

    // MOVE SPEED (nieuw)    
    public float GetMoveSpeed(float defaultValue = 30f)
    {
        return PlayerPrefs.GetFloat(KEY_MOVE_SPEED, defaultValue);
    }

    public void SetMoveSpeed(float speed)
    {
        PlayerPrefs.SetFloat(KEY_MOVE_SPEED, speed);
        ApplyMoveSpeed();
    }

    void ApplyMoveSpeed()
    {
        // Deze component zit op jouw PlayerController object (FirstPersonLocomotor)
        var fp = FindAnyObjectByType<FirstPersonLocomotor>();
        if (fp != null)
        {
            fp.SpeedFactor = GetMoveSpeed(30f);
        }
    }

    // APPLY ALL bij scene load    
    void ApplyAll()
    {
        ApplyTunneling();
        ApplyMoveSpeed();
    }
}
