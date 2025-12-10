using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent OnCrystalEnter;
    public UnityEvent OnCrystalExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Crystal>() != null)
            OnCrystalEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Crystal>() != null)
            OnCrystalExit.Invoke();
    }
}
