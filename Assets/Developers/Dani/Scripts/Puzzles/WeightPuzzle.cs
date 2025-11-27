using UnityEngine;
using UnityEngine.Events;

public class WeightPuzzle : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int requiredBlocks;
    [SerializeField] private int blocksOnPlate;
    private bool isCompleted;
    public UnityEvent onPuzzleCompleted;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weight"))
        {
            blocksOnPlate++;
            CheckPuzzleCompletion();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weight"))
        {
            blocksOnPlate--;
        }
    }

    private void CheckPuzzleCompletion()
    {
        if (!isCompleted && blocksOnPlate >= requiredBlocks)
        {
            isCompleted = true;
            onPuzzleCompleted.Invoke();
            Debug.Log("Puzzle Completed!");
            // Add additional logic for puzzle completion here
        }
    }
}
