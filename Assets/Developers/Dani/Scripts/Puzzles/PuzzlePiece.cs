using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider cd;
    private PuzzleSlot currentSlot;
    [Header("Settings")]
    [SerializeField] private float transitionDuration = 0.5f;
    public int puzzleID;

    public void PiecePlaced(PuzzleSlot puzzleSlot, Vector3 placedPosition, Quaternion placedRotation)
    {
        currentSlot = puzzleSlot;
        SetKinematic(true);
        StartCoroutine(PositionTransition(placedPosition, placedRotation));
    }

    private void SetKinematic(bool isKinematic)
    {
        rb.isKinematic = isKinematic;
        cd.enabled = !isKinematic;
    }

    private IEnumerator PositionTransition(Vector3 position, Quaternion rotation)
    {
        Vector3 startingPos = transform.position;
        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            transform.position = Vector3.Lerp(startingPos, position, elapsed / transitionDuration);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, elapsed / transitionDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = position;
        currentSlot.UpdateSlotPuzzle();
    }
}
