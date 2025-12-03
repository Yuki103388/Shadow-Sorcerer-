using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WandBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WandProjectile[] wandProjectiles;
    [SerializeField] Transform wandEndTrans;
    [SerializeField] Transform gemSlot;
    [SerializeField] LayerMask electricityLayer;
    private Crystal crystal;
    private RaycastHit hit;


    [Header("Settings")]
    [SerializeField] private float velocityRequiredToCast;
    [SerializeField] private float timeWindowToCheckVelocity = 0.2f;
    [SerializeField] private float latestHighestVelocity;
    private Vector3 lastPos;
    private Coroutine resetVelocityCoroutine;
    private int projectileIndex;

    private void Start()
    {
        lastPos = transform.position;
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

    private void FireRaycast()
    {
        // checks if the raycast hits an object that is on the electricty layer and reads the name, we could also do this in the elctric gem script
        if (Physics.Raycast(wandEndTrans.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity, electricityLayer))
            Debug.Log(hit.collider.name); //instead of the name we could get a function to activate from that object
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
