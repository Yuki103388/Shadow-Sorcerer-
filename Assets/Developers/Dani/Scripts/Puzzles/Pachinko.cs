using UnityEngine;

public class Pachinko : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody ballRigidbody;
    [SerializeField] private Transform top;
    [SerializeField] private Transform bottom;
    [Header("Settings")]
    [SerializeField] private Vector3 machineAngle;
    [SerializeField] private float shootForce;

    void Start()
    {
        machineAngle = top.position - bottom.position;
        machineAngle.Normalize();
    }

    [ContextMenu("Shoot Ball")]
    private void ShootBall()
    {
        ballRigidbody.AddForce(machineAngle * shootForce, ForceMode.Impulse);
    }
}
