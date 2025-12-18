using UnityEngine;

public class CogBehaviour : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float rotationDeceleration = 90f; //deceleration per second
    private float currentRotationSpeed;
    private bool isPowered;

    private void Update()
    {
        if(isPowered)
        {
            currentRotationSpeed = rotationSpeed;
            Debug.Log("turned on");

            isPowered = false;
        }

        if(currentRotationSpeed > 0)
        {
            currentRotationSpeed -= rotationDeceleration * Time.deltaTime;
            Debug.Log(currentRotationSpeed);

            Vector3 eulRot = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulRot.x, eulRot.y, eulRot.z - currentRotationSpeed * Time.deltaTime);
        }
    }

    [ContextMenu("Turn On Power")]
    public void TurnOnPower()
    {
        isPowered = true;
    }
}
