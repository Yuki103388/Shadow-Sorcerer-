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
    [SerializeField] GameObject telekinesisWandObject;
    public static WandBehaviour instance;
    private Crystal crystal;

    [Header("Settings")]
    [SerializeField] private float velocityRequiredToCast;
    [SerializeField] private float velocityWindow = 0.2f;
    [SerializeField] private float castCooldown = 0.5f;
    [SerializeField] private float lineRendererDuration = 0.5f;
    private float latestHighestVelocity;
    private Vector3 lastPos;
    private bool canCast = true;
    private int projectileIndex;
    private RaycastHit hit;
    private Coroutine resetVelocityCoroutine;
    private Coroutine lineRendererCoroutine;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

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
        yield return new WaitForSeconds(velocityWindow);
        latestHighestVelocity = 0f;
    }

    private IEnumerator CastCooldownTimer()
    {
        canCast = false;
        yield return new WaitForSeconds(castCooldown);
        canCast = true;
    }

    #region Primary Action

    public void ElementWandPrimaryResponse(InputAction.CallbackContext context)
    {
        if (context.performed && crystal != null && latestHighestVelocity >= velocityRequiredToCast && canCast)
        {
            StartCoroutine(CastCooldownTimer());
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
        projectile.gameObject.SetActive(true);
        projectile.Initialize(crystal.elementType, crystal.explosionRadius, crystal.explosionForce, crystal.projectilePrefab);
        projectile.transform.position = wandEndTrans.position;
        projectile.LaunchProjectile(wandEndTrans.transform.up * crystal.projectileSpeed);
        projectileIndex = (projectileIndex + 1) % wandProjectiles.Length;
    }

    [ContextMenu("Fire Raycast")]
    private void FireRaycast()
    {
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
            other.transform.eulerAngles = new Vector3(90, 0, 0);
            other.GetComponent<Rigidbody>().isKinematic = true;
            crystal = other.GetComponent<Crystal>();
            if (!crystal.isProjectile)
            {
                lineRenderer.material = crystal.lineRendererMaterial;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Crystal>() != null)
            RemoveCrystal();
    }

    public void RemoveCrystal()
    {
        if (crystal != null)
        {
            crystal.gameObject.transform.parent = null;
            crystal.GetComponent<Rigidbody>().isKinematic = false;
            crystal = null;
        }
    }

    #endregion

    public void SecondaryElementWandResponse(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            telekinesisWandObject.SetActive(true);
        }
        else if (context.canceled)
        {
            telekinesisWandObject.SetActive(false);
        }
    }
}
