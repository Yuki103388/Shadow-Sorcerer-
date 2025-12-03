using System.Collections;
using UnityEngine;

public class WandProjectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;

    [Header("Settings")]
    [SerializeField] private float lifeTime = 5f;
    private ElementType elementType;
    private float explosionRadius;

    private void OnEnable()
    {
        StartCoroutine(LifeTimer());
    }

    public void Initialize(ElementType type, float radius)
    {
        elementType = type;
        explosionRadius = radius;
    }

    private IEnumerator LifeTimer()
    {
        Debug.Log("Projectile Launched");
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Hit " + other.gameObject.name);
        if (other.gameObject.name == "Wand" || other.gameObject.GetComponent<Crystal>() != null) return;

        // if (other.GetComponent<receivingscript>() != null)
        // {
        //     other.GetComponent<receivingscript>().ReceiveHit();
        // }

        Explode();


        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {

    }


    private void Explode()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        Debug.Log("Projectile Exploded with " + hitColliders.Length + " hits.");
        for (int i = 0; i < hitColliders.Length; i++)
        {
            GameObject hitGameObject = hitColliders[i].gameObject;
            Vector3 hitDirection = hitGameObject.transform.position - transform.position;
            float hitDistance = Vector3.Distance(transform.position, hitGameObject.transform.position) / explosionRadius;


            // knock and move objects away if they have a rigidbody for fun :3
            if ((hitGameObject.gameObject.GetComponent<Rigidbody>() != null)
            && !hitGameObject.gameObject.GetComponent<Rigidbody>().isKinematic)
            {
                hitGameObject.gameObject.GetComponent<Rigidbody>().AddForce(hitDirection.normalized * 20 / (hitDistance + 0.5f), ForceMode.Force);
            }

            if (hitGameObject.GetComponent<ElementalInteractor>() != null)
            {
                hitGameObject.GetComponent<ElementalInteractor>().ElementHit(elementType);
            }

        }
    }

    public void LaunchProjectile(Vector3 velocity)
    {
        rb.linearVelocity = velocity;
    }

}
