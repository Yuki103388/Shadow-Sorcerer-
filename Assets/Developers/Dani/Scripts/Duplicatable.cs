using UnityEngine;
using UnityEngine.Events;

public class Duplicatable : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Collider objectCollider;
    public Duplicatable originalObject;
    [Header("Settings")]
    [SerializeField] private int maxDuplicates;
    public int currentDuplicates;
    public UnityEvent onDuplicate;

    [ContextMenu("Duplicate")]
    private void Duplicate()
    {
        if (originalObject == null)
        {
            if (!CanDuplicate()) return;
        }
        else if (!originalObject.CanDuplicate()) return;

        Duplicatable Newobject = Instantiate(gameObject, transform.position + (objectCollider.bounds.extents.y * Vector3.up * 2), transform.rotation).GetComponent<Duplicatable>();
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
