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
    FirstPersonLocomotor locomotor;

    void Start()
    {
        slider = GetComponent<Slider>();
        locomotor = FindAnyObjectByType<FirstPersonLocomotor>();

        float saved = PlayerPrefs.GetFloat("MoveSpeed", 30f);
        slider.SetValueWithoutNotify(Mathf.InverseLerp(minSpeed, maxSpeed, saved));

        Apply(saved);
        UpdateText(saved);

        slider.onValueChanged.AddListener(OnChanged);
    }

    void OnChanged(float value01)
    {
        float speed = Mathf.Lerp(minSpeed, maxSpeed, value01);
        PlayerPrefs.SetFloat("MoveSpeed", speed);

        Apply(speed);
        UpdateText(speed);
    }

    void Apply(float speed)
    {
        if (locomotor != null)
            locomotor.SpeedFactor = speed;
    }

    void UpdateText(float speed)
    {
        if (valueText != null)
            valueText.text = $"Move Speed: {speed:0}";
    }
}
