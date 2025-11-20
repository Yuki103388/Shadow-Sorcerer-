using System.Collections.Generic;
using UnityEngine;

public class SlotPuzzle : MonoBehaviour
{
    [SerializeField] private List<PuzzleSlot> puzzleSlots = new List<PuzzleSlot>(); //Automatically assigned. Put slots as children of this object
    [Header("Settings")]
    private int puzzlesCollected;

    //Stores PuzzleSlot children and gives them their IDS
    private void Awake()
    {
        puzzleSlots.AddRange(transform.GetComponentsInChildren<PuzzleSlot>());
    }

    public void PiecesCollected()
    {
        puzzlesCollected++;
        if (puzzlesCollected >= puzzleSlots.Count)
        {
            Debug.Log("Puzzle Completed!");
        }
    }

}
