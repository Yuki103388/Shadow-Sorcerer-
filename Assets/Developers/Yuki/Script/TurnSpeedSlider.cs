using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class TurnSpeedSlider : MonoBehaviour
{
    public TextMeshProUGUI valueText;

    public float minTurn = 30f;
    public float maxTurn = 180f;

    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        float saved = PlayerPrefs.GetFloat("TurnSpeed", 90f);
        slider.SetValueWithoutNotify(Mathf.InverseLerp(minTurn, maxTurn, saved));

        UpdateText(saved);
        slider.onValueChanged.AddListener(OnChanged);
    }

    void OnChanged(float value01)
    {
        float turn = Mathf.Lerp(minTurn, maxTurn, value01);
        PlayerPrefs.SetFloat("TurnSpeed", turn);

        UpdateText(turn);

        
    }

    void UpdateText(float turn)
    {
        if (valueText != null)
            valueText.text = $"Turn Speed: {turn:0}°";
    }
}
