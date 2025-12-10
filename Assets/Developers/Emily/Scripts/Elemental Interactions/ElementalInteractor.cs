using UnityEngine;
using UnityEngine.Events;

public class ElementalInteractor : MonoBehaviour
{
    public ElementType elementNeeded;
    public UnityEvent OnCorrectElement;

    public void ElementHit(ElementType hitType)
    {
        if (hitType == elementNeeded)
            OnCorrectElement.Invoke();
    }

    public void SetElementNeeded(ElementType newElement)
    {
        elementNeeded = newElement;
    }
}
