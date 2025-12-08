using Oculus.Interaction;
using UnityEngine;

public class FastObjectBehaviour : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody _rb;
    private ElementalInteractor _interactor;
    [Header("variables")]
    [SerializeField] private int _speed;
    private bool canMove = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _interactor = GetComponent<ElementalInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement(canMove);
    }

    // moves the object forwards multiplied by the speed var
    private void Movement(bool moving)
    {
        if (moving)
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }

    // if it hits a will flips the forward direction to the opposite side
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

    // stops the object and freezes it, sets the required element to be fire to set it free
    public void FreezeObject()
    {
        canMove = false;
        _interactor.elementNeeded = ElementType.Fire;
        _interactor.OnCorrectElement.AddListener(UnFreezeObject);
    }

    // makes the object fall and unfreeze it and makes it uninteractable for the wand
    private void UnFreezeObject()
    {
        _rb.isKinematic = false;
        _interactor.elementNeeded = ElementType.None;
        _interactor.OnCorrectElement = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.name)
        {
            case "wall": FlipDirection(); break;
        }
    }
}
