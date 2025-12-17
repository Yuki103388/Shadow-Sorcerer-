using Oculus.Interaction.Locomotion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class MoveSpeedSlider : MonoBehaviour
{
    public TextMeshProUGUI valueText;

    public float minSpeed = 10f;
    public float maxSpeed = 60f;

    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        slider.wholeNumbers = false;
        slider.minValue = 0f;
        slider.maxValue = 1f;

        // Laad opgeslagen speed via SettingsManager (of default 30)
        float savedSpeed = SettingsManager.Instance != null
            ? SettingsManager.Instance.GetMoveSpeed(30f)
            : PlayerPrefs.GetFloat("MoveSpeed", 30f);

        float saved01 = Mathf.InverseLerp(minSpeed, maxSpeed, savedSpeed);
        slider.SetValueWithoutNotify(saved01);

        UpdateText(savedSpeed);

        slider.onValueChanged.AddListener(OnChanged);

        // Apply meteen (voor de huidige scene)
        Apply(savedSpeed);
    }

    void OnChanged(float value01)
    {
        float speed = Mathf.Lerp(minSpeed, maxSpeed, value01);

        //Dit is de belangrijke fix:
        if (SettingsManager.Instance != null)
            SettingsManager.Instance.SetMoveSpeed(speed); // slaat op + apply in huidige scene
        else
            PlayerPrefs.SetFloat("MoveSpeed", speed);

        UpdateText(speed);
    }

    void Apply(float speed)
    {
        var locomotor = FindAnyObjectByType<FirstPersonLocomotor>();
        if (locomotor != null)
            locomotor.SpeedFactor = speed;
    }

    void UpdateText(float speed)
    {
        if (valueText != null)
            valueText.text = $"Move Speed: {speed:0}";
    }
}
