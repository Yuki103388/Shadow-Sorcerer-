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
        var max = Mathf.Max(_simonSays.selectedRunes.Count);
        if (eventData.button == PointerEventData.InputButton.Left &&_simonSays.selectedRunes[max -1] == _renderer)
        {
            selected = true;

        }else if (!_simonSays.selectedRunes.Contains(_renderer))
        {
            selected=false;
            _simonSays.selectedRunes.Clear();
            Debug.Log("failed");
        }
    }
}
