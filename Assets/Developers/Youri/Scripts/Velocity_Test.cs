using UnityEngine;

public class Velocity_Test : MonoBehaviour
{
    public float threshold = 2f;

    private Vector3 lastPos;

    void Start()
    {
        lastPos = transform.position;
    }

    void Update()
    {
        Vector3 vel = (transform.position - lastPos) / Time.deltaTime;

        if (vel.magnitude > threshold)
            Debug.Log("Manual velocity above threshold: " + vel.magnitude);

        lastPos = transform.position;
    }
}
