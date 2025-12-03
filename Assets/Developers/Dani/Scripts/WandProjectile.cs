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

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Wand") return;

        // if (other.GetComponent<receivingscript>() != null)
        // {
        //     other.GetComponent<receivingscript>().ReceiveHit();
        // }

        Physics.SphereCastAll(transform.position, explosionRadius, Vector3.zero);
        gameObject.SetActive(false);
    }

    public void LaunchProjectile(Vector3 velocity)
    {
        rb.linearVelocity = velocity;
    }

}
