using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class RuneSimonSays : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private List<RuneBehavuour> _runes;
    public List<RuneBehavuour> selectedRunes;
    private List<int> excludedElements =new List<int>();
    public bool gameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
    }



    public IEnumerator SimonSaysBehaviour()
    {
        while (selectedRunes.Count <= _runes.Count && !gameOver)
        {
            WaitForSeconds wait = new WaitForSeconds(1);
            if (selectedRunes.Count >= 3)
            {
                for (int i = 0; i < selectedRunes.Count; i++)
                {
                    selectedRunes[i].Selected();
                    yield return wait;
                    ResetRune();
                }
            }
            else
            {
                yield return null;
            }
            
            for (int i = 0; i < 3; i++)
            {
                int randomRune = Random.Range(0, _runes.Count);
                if (!excludedElements.Contains(randomRune))
                {
                    selectedRunes.Add(_runes[randomRune]);
                    _runes[randomRune].Selected();
                    yield return wait;
                    ResetRune();
                    excludedElements.Add(randomRune);
                }
            }
            yield return new WaitUntil(() =>
            {
                for (int i = 0; i < selectedRunes.Count; i++)
                {
                    if (!selectedRunes[i].selected)
                    {
                       return false;           
                    }
                }
                  return true;
            });
            ResetRune();
        }
    }
    public void ResetRune()
    {
        for (int j = 0; j < selectedRunes.Count; j++)
        {
            selectedRunes[j].selected = false;
            selectedRunes[j].Deselect();
        }
    }
    private void StartSimonSays()
    {
        gameOver = false;
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
