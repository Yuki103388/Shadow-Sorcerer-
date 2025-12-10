using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent OnCrystalEnter;
    public UnityEvent OnCrystalExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
            OnCrystalEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
            OnCrystalExit.Invoke();
    }
}
