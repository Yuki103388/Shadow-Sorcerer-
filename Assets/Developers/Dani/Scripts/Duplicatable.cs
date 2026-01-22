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
    [SerializeField] private bool spawnTowardsWand;
    private int currentDuplicates;
    public UnityEvent onDuplicate;

    [ContextMenu("Duplicate")]
    public void Duplicate()
    {
        if (originalObject == null)
        {
            if (!CanDuplicate()) return;
        }
        else if (!originalObject.CanDuplicate()) return;

        Duplicatable Newobject;
        if (spawnTowardsWand && WandBehaviour.instance != null)
        {
            // Sets the new object's position in the direction of the wand from the original based on its collider size
            Vector3 directionToWand = (WandBehaviour.instance.transform.position - transform.position).normalized;
            Newobject = Instantiate(gameObject, transform.position + directionToWand * (objectCollider.bounds.extents.magnitude * duplicateHeightMultiplier), transform.rotation).GetComponent<Duplicatable>();
            Debug.Log("Duplicated at " + Newobject.transform.position);
        }
        else
        {
            // Sets the new object's position above the original based on its collider size
            Newobject = Instantiate(gameObject, transform.position + (objectCollider.bounds.extents.y * Vector3.up * duplicateHeightMultiplier), transform.rotation).GetComponent<Duplicatable>();
        }

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

        onDuplicate.Invoke();
    }

    public bool CanDuplicate()
    {
        return currentDuplicates < maxDuplicates;
    }
}
