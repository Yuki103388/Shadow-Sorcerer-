using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class SmoothTunnelingToggle : MonoBehaviour
{
    Toggle toggle;

    void OnEnable()
    {
        toggle = GetComponent<Toggle>();

        // Sync UI met huidige setting
        toggle.SetIsOnWithoutNotify(SettingsManager.Instance.TunnelingEnabled);

        toggle.onValueChanged.RemoveListener(OnChanged);
        toggle.onValueChanged.AddListener(OnChanged);
    }

    void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(OnChanged);
    }

    void OnChanged(bool value)
    {
        SettingsManager.Instance.SetTunneling(value);
    }

    // Voor Meta XR Ray interaction
    public void Flip()
    {
        toggle.isOn = !toggle.isOn;
    }
}
