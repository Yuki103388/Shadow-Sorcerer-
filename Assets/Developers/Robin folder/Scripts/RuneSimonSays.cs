using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class RuneSimonSays : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private Renderer[] _runes;
    protected List<Renderer> _selectedRunes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       
    }

    private IEnumerator SimonSaysBehaviour()
    {
        while (_selectedRunes.Count != 0 || _selectedRunes.Count < _runes.Length)
        {
            int randomRune = Random.Range(0, _runes.Length);
            WaitForSeconds wait = new WaitForSeconds(2);
            Debug.Log("start sequence");
            _runes[randomRune].material.color = Color.white;
            _selectedRunes.Add(_runes[randomRune]);
            yield return wait;
            _runes[randomRune].material.color = Color.green;
            for (int i = 0; i < _selectedRunes.Count; i++)
            yield return new WaitUntil(() => _selectedRunes[i].GetComponent<RuneBehavuour>().selected == true);
        }
    }

   public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            StartCoroutine(SimonSaysBehaviour());
        }
    }
}
