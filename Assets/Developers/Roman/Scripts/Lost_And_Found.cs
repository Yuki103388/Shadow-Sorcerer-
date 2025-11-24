using UnityEngine;

public class Lost_And_Found : MonoBehaviour
{
    [SerializeField] private Transform _lostAndFound;
    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = _lostAndFound.position;
        Rigidbody rb = other.attachedRigidbody;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
