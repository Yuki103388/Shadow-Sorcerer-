using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using Meta.WitAi;

public class RuneSimonSays : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private List<RuneBehavuour> _runes;
    public List<RuneBehavuour> selectedRunes;
    [SerializeField ]private List<int> excludedElements =new List<int>();
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

            for (int i = 0; i < 3;)
            {
               int randomRune = Random.Range(0, _runes.Count);
               if (excludedElements.Contains(randomRune))
               {
                   randomRune= Random.Range(0, _runes.Count);
               }
               else 
               {
                   selectedRunes.Add(_runes[randomRune]);
                   _runes[randomRune].Selected();
                   yield return new WaitForSeconds(1);
                   ResetRune();
                   excludedElements.Add(randomRune);
                   i++;
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
            selectedRunes[j]._simonSayIndex = 0;
        }
    }
    private void StartSimonSays()
    {
        gameOver = false;
        excludedElements.Clear();
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
