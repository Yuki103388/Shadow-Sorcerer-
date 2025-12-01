using UnityEngine;
using UnityEngine.EventSystems;

public class RuneBehavuour : MonoBehaviour, IPointerDownHandler
{
    public bool selected = false;
    private Renderer _renderer;
    private RuneSimonSays _simonSays;
    private void Awake()
    {
        _simonSays = GetComponentInParent<RuneSimonSays>();
        _renderer = GetComponent<Renderer>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && _simonSays.selectedRunes.Contains(_renderer))
        {
            _renderer.material.color = Color.white;
            selected = true;

        }
        
        if (!_simonSays.selectedRunes.Contains(_renderer))
        {
            StopCoroutine(_simonSays.SimonSaysBehaviour());
            _simonSays.selectedRunes.Clear();
            _simonSays.GameOver();
            Debug.Log("failed");
        }
    }
}
