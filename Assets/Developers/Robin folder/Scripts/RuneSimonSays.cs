using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using Meta.WitAi;

public class RuneSimonSays : MonoBehaviour,IPointerDownHandler
{
    [Header("References")]
    [SerializeField] private List<RuneBehavuour> _runes;
    public List<RuneBehavuour> selectedRunes;
    private List<int> excludedElements =new List<int>();

    [Header("variables")]
    public bool gameOver = false;
    private bool sequence = false;

    [Header("Components")]
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public IEnumerator SimonSaysBehaviour()
    {
        while (!gameOver && selectedRunes.Count <= _runes.Count )
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
                   yield return wait;
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
            if (!gameOver)
            {
                _renderer.material.color = Color.green;
                yield return new WaitForSeconds(2);
                _renderer.material.color = Color.blue;
            }
        }
    }
    public IEnumerator GameOver()
    {
        gameOver = true;
        ResetRune();
        selectedRunes.Clear();
        _renderer.material.color = Color.red;
        yield return new WaitForSeconds(2);
        _renderer.material.color = Color.gray;
        sequence = false;
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
        if (!sequence)
        {
            gameOver = false;
            excludedElements.Clear();
            StartCoroutine(SimonSaysBehaviour());
            _renderer.material.color = Color.blue;
            sequence = true;
        }
    }
   public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            StartSimonSays();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag) {
            case "Hand": StartSimonSays(); break;
        }
    }
}
