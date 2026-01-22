using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

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
        Transform pieceTransform = piece.transform;
        pieceTransform.SetParent(transform);
        piece.PiecePlaced(this, placedPosition + transform.position, transform.rotation);
    }

    public void UpdateSlotPuzzle()
    {
        slotPuzzle.PiecesCollected();
    }
}
