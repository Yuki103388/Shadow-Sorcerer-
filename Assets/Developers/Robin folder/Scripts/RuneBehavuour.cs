using UnityEngine;
using UnityEngine.EventSystems;

public class RuneBehavuour : MonoBehaviour, IPointerDownHandler
{
    public bool selected = false;
    private Renderer _renderer;
    private RuneSimonSays _simonSays;
    private int _simonSayIndex;

    private void Awake()
    {
        _simonSays = GetComponentInParent<RuneSimonSays>();
        _renderer = GetComponent<Renderer>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && _simonSays.selectedRunes[_simonSayIndex] == _renderer)
        {
            for (int i = 0; i < _simonSays.selectedRunes.Count; i++)
            {
                
            }
            _renderer.material.color = Color.white;
            selected = true;
            Debug.Log(_simonSays.selectedRunes[_simonSayIndex]);
        }else 
        {
            StopCoroutine(_simonSays.SimonSaysBehaviour());
            _simonSays.ResetRune();
            _simonSays.selectedRunes.Clear();
            _simonSays.gameOver = true;
        }
    }

    public void Selected()
    {
        _renderer.material.color = Color.white;
    }
    public void Deselect()
    {
        _renderer.material.color = Color.green;
    }

}
