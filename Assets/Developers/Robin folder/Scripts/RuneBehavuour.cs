using UnityEngine;
using UnityEngine.EventSystems;
using static Unity.Collections.Unicode;

public class RuneBehavuour : MonoBehaviour, IPointerDownHandler
{
    [Header("Components")]
    private Renderer _renderer;
    private Renderer _startRuneRenderer;
    private RuneSimonSays _simonSays;
    private RuneBehavuour _runeBehaviour;
    [Header("variables")]
    public bool selected = false;
    public int _simonSayIndex;

    private void Awake()
    {
        _runeBehaviour = GetComponent<RuneBehavuour>();
        _simonSays = GetComponentInParent<RuneSimonSays>();
        _startRuneRenderer = _simonSays.gameObject.GetComponent<Renderer>();
        _renderer = GetComponent<Renderer>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && _simonSays.selectedRunes[_simonSayIndex] == _runeBehaviour&&!_simonSays.gameOver)
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
            _startRuneRenderer.material.color = Color.red;
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
