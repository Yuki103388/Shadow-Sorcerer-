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
    public bool isRunning = false;
    private bool sequence = false;

    [Header("Components")]
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Starts the game, it selects 3 runes from the list and adds them to the current sequence. Then checks if you press it ín the correct order
    public IEnumerator SimonSaysBehaviour()
    {
        // checks if you are on the first pattern, if yes it does not turn the previous runes on because there are none. Otherwise makes the previous pattern and runes turn on for a second.
        while (!gameOver && selectedRunes.Count < _runes.Count )
        {
            WaitForSeconds wait = new WaitForSeconds(1);
            if (selectedRunes.Count >= 3)
            {
                isRunning = true;
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
            // chooses a random rune if that rune has already been excluded choose a different one, then adds it to the selected runes and excludes it from being picked again.
            for (int i = 0; i < 3;)
            {
               isRunning = true;
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
            // The courotine waits until all chosen runes have been selected by the player
            yield return new WaitUntil(() =>
            {
                isRunning = false;
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
        Winbehaviour();
    }
    // Sets the game state on gameover,stops the current sequence and makes the start button flash red, then puts it back to normal.

    private void Winbehaviour()
    {
        //add win behaviour like spawing a key or rune


        StopAllCoroutines();
    }
    public IEnumerator GameOver()
    {
        gameOver = true;
        ResetRune();
        selectedRunes.Clear();
        _renderer.material.color = Color.red;
        yield return new WaitForSeconds(2);
        _renderer.material.color = Color.white;
        sequence = false;
        isRunning = false;
    }
    // removes all the selected runes and resets the index variable
    public void ResetRune()
    {
        for (int j = 0; j < selectedRunes.Count; j++)
        {
            selectedRunes[j].selected = false;
            selectedRunes[j].Deselect();
            selectedRunes[j]._simonSayIndex = 0;
        }
    }
    // checks if the game is already running, if not reset all state variables and start the game courotine
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
    // CHecks for the mouse interactions
   public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            StartSimonSays();
        }
    }
    // checks for the VR interactions
    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag) {
            case "Hand": StartSimonSays(); break;
        }
    }
}
