using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WandBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private WandProjectile[] wandProjectiles;
    [SerializeField] Transform wandEndTrans;
    [SerializeField] Transform gemSlot;
    // [SerializeField] LayerMask electricityLayer;
    private Crystal crystal;
    private RaycastHit hit;


    [Header("Settings")]
    [SerializeField] private float velocityRequiredToCast;
    [SerializeField] private float timeWindowToCheckVelocity = 0.2f;
    [SerializeField] private float latestHighestVelocity;
    [SerializeField] private float lineRendererDuration = 0.5f;
    private Vector3 lastPos;
    private Coroutine resetVelocityCoroutine;
    private Coroutine lineRendererCoroutine;
    private int projectileIndex;

    private void Start()
    {
        lastPos = transform.position;
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, wandEndTrans.position);
    }

    private void FixedUpdate()
    {
        Vector3 velocity = (transform.position - lastPos) / Time.fixedDeltaTime;
        if (velocity.magnitude > latestHighestVelocity)
        {
            latestHighestVelocity = velocity.magnitude;
            if (resetVelocityCoroutine != null)
                StopCoroutine(resetVelocityCoroutine);
            resetVelocityCoroutine = StartCoroutine(ResetVelocityCheck());
        }

        lastPos = transform.position;
    }

    private IEnumerator ResetVelocityCheck()
    {
        yield return new WaitForSeconds(timeWindowToCheckVelocity);
        latestHighestVelocity = 0f;
    }


    public void ElementWandResponse(InputAction.CallbackContext context)
    {
        //Checks the element gem slot, then executes that specific function. Easy to expand.
        if (context.performed && crystal != null && latestHighestVelocity >= velocityRequiredToCast)
        {
            if (crystal.isProjectile)
            {
                FireProjectile();
            }
            else
            {
                FireRaycast();
            }
        }
    }

    [ContextMenu("Fire Projectile")]
    private void FireProjectile()
    {
        WandProjectile projectile = wandProjectiles[projectileIndex];
        projectile.Initialize(crystal.elementType, crystal.explosionRadius);
        projectile.gameObject.SetActive(true);
        projectile.transform.position = wandEndTrans.position;
        projectile.LaunchProjectile(wandEndTrans.transform.up * crystal.projectileSpeed);
        projectileIndex = (projectileIndex + 1) % wandProjectiles.Length;
    }

    [ContextMenu("Fire Raycast")]
    private void FireRaycast()
    {
        // checks if the raycast hits an object that is on the electricty layer and reads the name, we could also do this in the elctric gem script
        if (Physics.Raycast(wandEndTrans.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.GetComponent<ElementalInteractor>() != null)
                hit.collider.gameObject.GetComponent<ElementalInteractor>().ElementHit(crystal.elementType);
            lineRendererCoroutine = StartCoroutine(LineRendererTimer(hit.point));
        }
    }

    private IEnumerator LineRendererTimer(Vector3 hitPos)
    {
        lineRenderer.SetPosition(1, hitPos);
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(lineRendererDuration);
        lineRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Crystal>() != null)
        {
            other.gameObject.transform.parent = gemSlot;
            other.transform.position = gemSlot.position;
            other.GetComponent<Rigidbody>().isKinematic = true;
            crystal = other.GetComponent<Crystal>();
        }
    }
}
