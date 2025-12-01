using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class RuneSimonSays : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] private List<Renderer> _runes;
    public List<Renderer> selectedRunes = new List<Renderer>();
    private List<Renderer> _originalRunes = new List<Renderer>();
    private bool _gameOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _originalRunes.AddRange(_runes);
    }

    public IEnumerator SimonSaysBehaviour()
    {
        while (selectedRunes.Count <= _originalRunes.Count && !_gameOver)
        {
            Debug.Log("nee");
            for (int i = 0; i < 3; i++)
            {
                WaitForSeconds wait = new WaitForSeconds(1);
                int randomRune = Random.Range(0, _runes.Count);
                selectedRunes.Add(_runes[randomRune]);
                _runes[randomRune].material.color = Color.white;
                yield return wait;
                _runes[randomRune].material.color = Color.green;
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
            Debug.Log("continue");
            ResetRune();
        }
    }

    public void GameOver()
    {
        ResetRune();
        _gameOver = true;
    }

    private void ResetRune()
    {
        for (int j = 0; j < selectedRunes.Count; j++)
        {
            selectedRunes[j].GetComponent<RuneBehavuour>().selected = false;
            selectedRunes[j].material.color = Color.green;
        }
    }
    private void RestartSimonSays()
    {
        Debug.Log("ja");
        _runes.Clear();
        selectedRunes.Clear();
        _runes.AddRange(_originalRunes);
        StartCoroutine(SimonSaysBehaviour());
    }
   public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            RestartSimonSays();
        }
    }
}
