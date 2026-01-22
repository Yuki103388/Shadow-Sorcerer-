using System.Collections.Generic;
using UnityEngine;

public class WandTelekinesis : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float pullStrength = 10f;

    public List<Rigidbody> telekinesisObjects = new List<Rigidbody>();

    private void AttractObject(Rigidbody rb)
    {
        rb.useGravity = false;
        telekinesisObjects.Add(rb);
    }

    private void LetGoObject(Rigidbody rb)
    {
        rb.useGravity = true;
        telekinesisObjects.Remove(rb);
    }

    private void PullObjects()
    {
        foreach (Rigidbody rb in telekinesisObjects)
        {
            Vector3 directionToWand = (transform.position - rb.position);
            float distance = directionToWand.magnitude;
            Vector3 force = directionToWand.normalized * pullStrength;
            rb.AddForce(force, ForceMode.Acceleration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null && other.GetComponent<Rigidbody>().isKinematic == false)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            AttractObject(rb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            LetGoObject(rb);
        }
    }

    private void OnDisable()
    {
        foreach (Rigidbody rb in telekinesisObjects)
        {
            rb.useGravity = true;
        }
        telekinesisObjects.Clear();
    }

    private void FixedUpdate()
    {
        PullObjects();
    }
}
