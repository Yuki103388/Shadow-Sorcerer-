using UnityEngine;

public class SpinWheel : MonoBehaviour
{
    SpinningWheelBehaviour spinScript;

    private void Awake()
    {
        spinScript = GetComponentInParent<SpinningWheelBehaviour>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            StartCoroutine(spinScript.SpinWheel());
        }
    }
}
