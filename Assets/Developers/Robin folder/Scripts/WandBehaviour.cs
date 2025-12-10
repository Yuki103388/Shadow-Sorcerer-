using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WandBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private WandProjectile[] wandProjectiles;
    [SerializeField] private Transform wandEndTrans;
    [SerializeField] private Transform gemSlot;

    public static WandBehaviour instance;

    private Crystal crystal;
    private RaycastHit hit;

    [Header("Settings")]
    [SerializeField] private float velocityRequiredToCast = 2f;
    [SerializeField] private float timeWindowToCheckVelocity = 0.2f;
    [SerializeField] private float lineRendererDuration = 0.5f;
    [SerializeField] private float castCooldown = 0.25f;

    private float latestHighestVelocity;
    private Vector3 lastPos;
    private Coroutine resetVelocityCoroutine;
    private Coroutine lineRendererCoroutine;
    private bool canCast = true;
    private int projectileIndex = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        lastPos = transform.position;

        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
            lineRenderer.positionCount = 2;
        }
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

        //  AUTO-CAST WHEN VELOCITY THRESHOLD IS HIT
        if (crystal != null && canCast && latestHighestVelocity >= velocityRequiredToCast)
        {
            AutoCast();
        }

        lastPos = transform.position;
    }

    private IEnumerator ResetVelocityCheck()
    {
        yield return new WaitForSeconds(timeWindowToCheckVelocity);
        latestHighestVelocity = 0f;
    }

    private void AutoCast()
    {
        canCast = false;   // prevent multi-cast spam
        StartCoroutine(CastCooldownTimer());

        if (crystal.isProjectile)
            FireProjectile();
        else
            FireRaycast();
    }

    private IEnumerator CastCooldownTimer()
    {
        yield return new WaitForSeconds(castCooldown);
        canCast = true;
    }

    private void FireProjectile()
    {
        if (wandProjectiles.Length == 0) return;

        WandProjectile projectile = wandProjectiles[projectileIndex];
        projectile.Initialize(crystal.elementType, crystal.explosionRadius);

        projectile.transform.position = wandEndTrans.position;
        projectile.gameObject.SetActive(true);

        projectile.LaunchProjectile(wandEndTrans.up * crystal.projectileSpeed);

        projectileIndex = (projectileIndex + 1) % wandProjectiles.Length;
    }

    private void FireRaycast()
    {
        Vector3 dir = wandEndTrans.up;

        if (Physics.Raycast(wandEndTrans.position, dir, out hit, Mathf.Infinity))
        {
            ElementalInteractor interactor = hit.collider.GetComponent<ElementalInteractor>();
            if (interactor != null)
                interactor.ElementHit(crystal.elementType);

            if (lineRendererCoroutine != null)
                StopCoroutine(lineRendererCoroutine);

            lineRendererCoroutine = StartCoroutine(LineRendererTimer(hit.point));
        }
    }

    private IEnumerator LineRendererTimer(Vector3 hitPos)
    {
        lineRenderer.SetPosition(0, wandEndTrans.position);
        lineRenderer.SetPosition(1, hitPos);

        lineRenderer.enabled = true;
        yield return new WaitForSeconds(lineRendererDuration);
        lineRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Crystal newCrystal = other.GetComponent<Crystal>();
        if (newCrystal == null) return;

        newCrystal.transform.SetParent(gemSlot);
        newCrystal.transform.localPosition = Vector3.zero;
        newCrystal.GetComponent<Rigidbody>().isKinematic = true;

        crystal = newCrystal;
    }
}
