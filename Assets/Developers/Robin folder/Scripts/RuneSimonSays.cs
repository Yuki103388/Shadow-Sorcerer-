using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class RuneSimonSays : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private List<Renderer> _runes;
    public List<Renderer> selectedRunes = new List<Renderer>();
    private List<Renderer> _originalRunes = new List<Renderer>();
    public bool gameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _originalRunes.AddRange(_runes);
    }

    public IEnumerator SimonSaysBehaviour()
    {
        while (selectedRunes.Count <= _originalRunes.Count && !gameOver)
        {
            WaitForSeconds wait = new WaitForSeconds(1);
            for (int i = 0; i < selectedRunes.Count; i++)
            {
                if (selectedRunes.Count <= 3)
                {
                    selectedRunes[i].material.color = Color.white;
                    yield return wait;
                    ResetRune();
                }
                else
                {
                    yield return null;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                int randomRune = Random.Range(0, _runes.Count);
                selectedRunes.Add(_runes[randomRune]);           
                _runes[randomRune].material.color = Color.white;
                yield return wait;
                ResetRune();
                _runes.RemoveAt(randomRune);
            }
            yield return new WaitUntil(() =>
            {
                for (int i = 0; i < selectedRunes.Count; i++)
                {
                    if (!selectedRunes[i].GetComponent<RuneBehavuour>().selected)
                    {
                       return false;           
                    }
                }
                  return true;
            });
            ResetRune();
        }
    }
    private void ResetRune()
    {
        for (int j = 0; j < selectedRunes.Count; j++)
        {
            selectedRunes[j].GetComponent<RuneBehavuour>().selected = false;
            selectedRunes[j].material.color = Color.green;
        }
    }
    private void StartSimonSays()
    {
        gameOver = false;
        _runes.Clear();
        selectedRunes.Clear();
        _runes.AddRange(_originalRunes);
        StartCoroutine(SimonSaysBehaviour());
    }
   public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            StartSimonSays();
        }
    }
}
