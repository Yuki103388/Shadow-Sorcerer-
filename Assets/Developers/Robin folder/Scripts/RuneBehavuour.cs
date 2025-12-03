using UnityEngine;
using UnityEngine.EventSystems;
using static Unity.Collections.Unicode;

public class RuneBehavuour : MonoBehaviour, IPointerDownHandler
{
    [Header("Components")]
    private Renderer _renderer;
    private RuneSimonSays _simonSays;
    private RuneBehavuour _runeBehaviour;
    [Header("variables")]
    public bool selected = false;
    public int _simonSayIndex;

    private void Awake()
    {
        _runeBehaviour = GetComponent<RuneBehavuour>();
        _simonSays = GetComponentInParent<RuneSimonSays>();
        _renderer = GetComponent<Renderer>();
    }

    public void OnPointerDown(PointerEventData eventData)
    { 
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnRuneSelectedBehaviour();
        }   
    }

    private void OnRuneSelectedBehaviour()
    {
        // checks if the selected rune is the next one in the sequence and updates the index to be the one after in the sequence, if thats not the case reset all the runes
        if (_simonSays.selectedRunes[_simonSayIndex] == _runeBehaviour && !_simonSays.gameOver)
        {
            for (int i = 0; i < _simonSays.selectedRunes.Count; i++)
            {
                _simonSays.selectedRunes[i]._simonSayIndex++;
            }
            Selected();
            selected = true;
        }
        else
        {
            StartCoroutine(_simonSays.GameOver());
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

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag) {
            case "Hand": OnRuneSelectedBehaviour(); break;
        }
    }

}
