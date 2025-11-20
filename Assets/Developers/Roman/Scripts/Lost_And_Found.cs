using UnityEngine;

public class Lost_And_Found : MonoBehaviour
{
    [SerializeField] private Transform _lostAndFound;
    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.position = _lostAndFound.position;
    }
}
