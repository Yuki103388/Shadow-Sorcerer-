using System.Runtime.InteropServices.WindowsRuntime;
using Meta.XR.ImmersiveDebugger.UserInterface;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class SettingManager : MonoBehaviour
{
    [SerializeField] private GameObject vignetteObject;

    private Toggle uiToggle;

    private void Start()
    {
        uiToggle = GetComponent<Toggle>();
        if (uiToggle != null)
        {
            uiToggle.onValueChanged.AddListener(ToggleVignette);
        }
    }

    public void ToggleVignette(bool state)
    {
        if (vignetteObject != null)
        {
            vignetteObject.SetActive(state);
        }
        else
        {
            Debug.LogWarning("SettingManager: vignetteObject is not assigned.");
        }

        Debug.Log("Vignette Toggle State: " + state);
    }
}
