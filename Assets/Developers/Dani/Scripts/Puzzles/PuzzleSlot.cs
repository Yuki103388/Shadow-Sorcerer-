using System.ComponentModel;
using UnityEngine;

public class PuzzleSlot : MonoBehaviour
{
    [Header("References")]
    public SlotPuzzle slotPuzzle;
    [Header("Settings")]
    public int puzzleID;
    [Tooltip("0,0,0 is center of slot")]
    public Vector3 placedPosition;
    public Quaternion placedRotation;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PuzzlePiece>() != null)
        {
            PuzzlePiece piece = other.GetComponent<PuzzlePiece>();
            if (piece.puzzleID == puzzleID)
            {
                CollectPiece(piece);
            }
        }
    }

    private void CollectPiece(PuzzlePiece piece)
    {
        piece.transform.position = placedPosition + transform.position; //Replace with transition later
        piece.transform.rotation = placedRotation; //Replace with transition later
        piece.SetKinematic(true);
        SlotPuzzle slotPuzzle = GetComponentInParent<SlotPuzzle>();
        if (slotPuzzle != null)
        {
            slotPuzzle.PiecesCollected();
        }
    }
}
