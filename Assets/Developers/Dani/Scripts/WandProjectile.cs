using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class WandProjectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;
    private GameObject visuals;

    [Header("Settings")]
    [SerializeField] private float lifeTime = 5f;
    private float explosionForce;
    private ElementType elementType;
    private float explosionRadius;

    private void OnEnable()
    {
        StartCoroutine(LifeTimer());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        Destroy(visuals);
    }

    public void Initialize(ElementType type, float radius, float force, GameObject visualPrefab)
    {
        elementType = type;
        explosionRadius = radius;
        explosionForce = force;
        visuals = Instantiate(visualPrefab, transform.position, Quaternion.identity, transform);
    }

    private IEnumerator LifeTimer()
    {
        Debug.Log("Projectile Launched");
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Wand" || other.gameObject.GetComponent<Crystal>() != null) return;

        Debug.Log("Hit " + other.gameObject.name);

        Explode();
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
            )
            {
                hitGameObject.gameObject.GetComponent<Rigidbody>().AddForce(hitDirection.normalized * explosionForce / (hitDistance + 0.5f), ForceMode.Impulse);
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
