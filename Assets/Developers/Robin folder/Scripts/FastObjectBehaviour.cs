using Oculus.Interaction;
using UnityEngine;

public class FastObjectBehaviour : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody _rb;
    [Header("variables")]
    [SerializeField] private int _speed;
    private bool canMove = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement(canMove);
    }

    private void Movement(bool moving)
    {
        if (moving)
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }

    private void FlipDirection()
    {
        if (transform.position.z > 0)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void StopFastObject()
    {
        canMove = false;
        _rb.isKinematic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.name)
        {
            case "wall": FlipDirection(); break;
        }
    }
}
