using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;
    [Header("Settings")]
    public int puzzleID;

    public void SetKinematic(bool isKinematic)
    {
        _rigidbody.isKinematic = isKinematic;
        _collider.enabled = !isKinematic;
    }
}
