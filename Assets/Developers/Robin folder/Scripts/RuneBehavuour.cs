using UnityEngine;
using UnityEngine.EventSystems;

public class RuneBehavuour : RuneSimonSays, IPointerDownHandler
{
    public bool selected = false;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void OnPointerDownRune(PointerEventData eventData)
    {
        Debug.Log("click");
        if (eventData.button == PointerEventData.InputButton.Left &&_selectedRunes.Contains(_renderer))
        {
            selected = true;

        }else if (!_selectedRunes.Contains(_renderer))
        {
            selected=false;
            _selectedRunes.Clear();
        }
    }
}
