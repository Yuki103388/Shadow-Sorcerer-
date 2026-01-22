using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Events;

public class ElementalInteractor : MonoBehaviour
{
    public ElementType elementNeeded;
    public UnityEvent OnCorrectElement;

    [Header("Meta Grab")]
    [SerializeField] private GrabInteractable grabbable;

    private void Awake()
    {
        // Auto-assign if not set
        if (grabbable == null)
            grabbable = GetComponent<GrabInteractable>();

      // enable at start
        if (grabbable != null)
            grabbable.enabled = true;
    }

    public void ElementHit(ElementType hitType)
    {
        if (hitType != elementNeeded)
            return;

        // disable Meta grab
        if (grabbable != null)
            grabbable.enabled = false;

        OnCorrectElement.Invoke();
    }

    public void SetElementNeeded(ElementType newElement)
    {
        elementNeeded = newElement;
    }
}
