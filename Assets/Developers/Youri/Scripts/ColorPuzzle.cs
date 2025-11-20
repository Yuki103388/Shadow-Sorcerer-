using UnityEngine;

public class ColorPuzzle : MonoBehaviour
{
    public string correctTag;       // e.g. "Red" or "Green"
    public Transform snapPoint;     // Where the cube should snap
    public float snapSpeed = 10f;   // Smooth snap speed

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(correctTag))
        {
            Debug.Log("Correct object detected. Snapping...");

            // Stop cube movement
            Rigidbody rb = other.attachedRigidbody;
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.useGravity = false;
            }
            

                // Start snap
                StartCoroutine(SnapObject(other.transform));
        }
        else
        {
            
            Debug.Log("Wrong object entered.");

            // Eject the object
            Rigidbody rb = other.attachedRigidbody;
            if (rb != null)
            {
                Vector3 ejectDirection = (other.transform.position - transform.position).normalized;
                float ejectForce = 10f; // Adjust force as needed
                rb.useGravity = true;
                rb.AddForce(ejectDirection * ejectForce, ForceMode.Impulse);
            }
        }
    }

    private System.Collections.IEnumerator SnapObject(Transform obj)
    {
        while (Vector3.Distance(obj.position, snapPoint.position) > 0.01f)
        {
            obj.position = Vector3.Lerp(obj.position, snapPoint.position, Time.deltaTime * snapSpeed);
            obj.rotation = Quaternion.Lerp(obj.rotation, snapPoint.rotation, Time.deltaTime * snapSpeed);
            yield return null;
        }

        // Final exact snap
        obj.position = snapPoint.position;
        obj.rotation = snapPoint.rotation;
    }
}
