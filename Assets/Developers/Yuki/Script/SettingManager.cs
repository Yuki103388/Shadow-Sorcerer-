    using System.Runtime.InteropServices.WindowsRuntime;
    using Meta.XR.ImmersiveDebugger.UserInterface;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;


    public class SettingManager : MonoBehaviour
    {
        public static SettingManager Instance { get; private set; }

        [SerializeField] private GameObject vignetteObject;

        private Toggle uiToggle;
        private bool vignetteEnabled;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            uiToggle = GetComponent<Toggle>();
            if (uiToggle != null)
            {
                uiToggle.isOn = vignetteEnabled;
                uiToggle.onValueChanged.AddListener(ToggleVignette);
            }

            ApplyVignetteToObject();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Try to find vignette object in the new scene if inspector reference is null
            if (vignetteObject == null)
            {
                // Prefer tagging the vignette GameObject with "Vignette"
                vignetteObject = GameObject.FindWithTag("Vignette") ?? GameObject.Find("Vignette");
            }

            // Re-bind UI toggle in the new scene (if a Toggle exists for the setting)
            uiToggle = FindAnyObjectByType<Toggle>();
            if (uiToggle != null)
            {
                uiToggle.onValueChanged.RemoveListener(ToggleVignette);
                uiToggle.isOn = vignetteEnabled;
                uiToggle.onValueChanged.AddListener(ToggleVignette);
            }

            ApplyVignetteToObject();
        }

        public void ToggleVignette(bool state)
        {
            vignetteEnabled = state;
            ApplyVignetteToObject();
            Debug.Log("Vignette Toggle State: " + state);
        }

        private void ApplyVignetteToObject()
        {
            if (vignetteObject != null)
            {
                vignetteObject.SetActive(vignetteEnabled);
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                SceneManager.sceneLoaded -= OnSceneLoaded;
            }
        }
    }
