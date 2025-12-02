using UnityEngine;
using UnityEngine.EventSystems;
using static Unity.Collections.Unicode;

public class RuneBehavuour : MonoBehaviour, IPointerDownHandler
{
    public bool selected = false;
    private Renderer _renderer;
    private RuneSimonSays _simonSays;
    public int _simonSayIndex;

    private void Awake()
    {
        _simonSays = GetComponentInParent<RuneSimonSays>();
        _renderer = GetComponent<Renderer>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && _simonSays.selectedRunes[_simonSayIndex])
        {
            for(int i =0;i< _simonSays.selectedRunes.Count; i++)
            {
                _simonSays.selectedRunes[i]._simonSayIndex++;
            }
            Selected();
            selected = true;
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
