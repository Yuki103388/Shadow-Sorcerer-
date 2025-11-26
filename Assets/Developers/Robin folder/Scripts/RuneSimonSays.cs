using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class RuneSimonSays : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private List<Renderer> _runes;
    public List<Renderer> selectedRunes = new List<Renderer>();
    private List<Renderer> _originalRunes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _originalRunes = _runes;
    }

    private IEnumerator SimonSaysBehaviour()
    {
        while (selectedRunes.Count < _runes.Count)
        {
            int randomRune = Random.Range(0, _runes.Count);
            WaitForSeconds wait = new WaitForSeconds(2);
            _runes[randomRune].material.color = Color.white;
            selectedRunes.Add(_runes[randomRune]);
            yield return wait;
            _runes[randomRune].material.color = Color.green;
            _runes.RemoveAt(randomRune);
            for (int i = 0; i < selectedRunes.Count; i++)
            yield return new WaitUntil(() => selectedRunes[i].GetComponent<RuneBehavuour>().selected == true);
        }
    }

   public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            _runes = _originalRunes;
            StartCoroutine(SimonSaysBehaviour());
        }
    }
}
