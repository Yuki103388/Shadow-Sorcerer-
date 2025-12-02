using UnityEngine;
using UnityEngine.Events;

public class Duplicatable : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Collider objectCollider;
    private Duplicatable originalObject;
    [Header("Settings")]
    [SerializeField] private int maxDuplicates;
    [Tooltip("The multiplier of the distance the new object is placed from the original. 2 is next to original")]
    [SerializeField] private float duplicateHeightMultiplier = 2f;
    private int currentDuplicates;
    public UnityEvent onDuplicate;

    [ContextMenu("Duplicate")]
    private void Duplicate()
    {
        if (originalObject == null)
        {
            if (!CanDuplicate()) return;
        }
        else if (!originalObject.CanDuplicate()) return;

        // Sets the new object's position above the original based on its collider size
        Duplicatable Newobject = Instantiate(gameObject, transform.position + (objectCollider.bounds.extents.y * Vector3.up * duplicateHeightMultiplier), transform.rotation).GetComponent<Duplicatable>();
        onDuplicate.Invoke();
        if (originalObject == null)
        {
            currentDuplicates++;
            Newobject.originalObject = this;
        }
        else
        {
            originalObject.currentDuplicates++;
            Newobject.originalObject = originalObject;
        }
    }

    public bool CanDuplicate()
    {
        return currentDuplicates < maxDuplicates;
    }
}
