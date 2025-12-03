using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WandBehaviour : MonoBehaviour
{
    [Header("References")]
    private Crystal crystal;
    [SerializeField] Transform wandEndTrans;
    [SerializeField] Transform gemSlot;
    private RaycastHit hit;
    [SerializeField] LayerMask electricityLayer;


    [Header("Settings")]
    [SerializeField] private float velocityRequiredToCast;
    [SerializeField] private float timeWindowToCheckVelocity = 0.2f;
    [SerializeField] private float latestHighestVelocity;
    private Vector3 lastPos;
    private Coroutine resetVelocityCoroutine;

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
        if (context.performed && latestHighestVelocity >= velocityRequiredToCast)
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

    private void FireProjectile()
    {

    }

    private void FireRaycast()
    {
        // checks if the raycast hits an object that is on the electricty layer and reads the name, we could also do this in the elctric gem script
        if (Physics.Raycast(wandEndTrans.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity, electricityLayer))
            Debug.Log(hit.collider.name); //instead of the name we could get a function to activate from that object
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gem"))
        {
            other.gameObject.transform.parent = gemSlot;
            other.transform.position = Vector3.zero;
            crystal = other.GetComponent<Crystal>();
        }
    }
}
