using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected bool isBeingHeld = false;

    public void PickUp()
    {
        if (!isBeingHeld)
        {
            isBeingHeld = true;
        }
    }

    public void Drop()
    {
        if (isBeingHeld)
        {
            isBeingHeld = false;
        }
    }
}
