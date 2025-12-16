using Oculus.Interaction;
using UnityEngine;

public class FastObjectBehaviour : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody _rb;
    private ElementalInteractor _interactor;
    private Renderer _renderer;
    [SerializeField] Material _iceMaterial;
    [SerializeField] Material _baseMaterial;
    [Header("variables")]
    private bool _canInteract = false;
    [SerializeField] private int _speed;
    private bool canMove = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
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

    // stops the object and freezes it, sets the required element to be fire to unfreeze it
    public void FreezeObject()
    {
        canMove = false;
        _rb.isKinematic = false;
        _renderer.material = _iceMaterial;
        _interactor.elementNeeded = ElementType.Fire;
        _interactor.OnCorrectElement.AddListener(UnFreezeObject);
    }

    // makes the object fall and unfreeze it and makes it uninteractable for the wand
    private void UnFreezeObject()
    {
        _canInteract = true;
        _renderer.material = _baseMaterial;
        _interactor.elementNeeded = ElementType.None;
        _interactor.OnCorrectElement = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        // change this to tag
        switch (other.tag)
        {
            case "Wall": FlipDirection(); break;
            case "Hands":
                if (_canInteract)
                {
                    //add code to interact with vr hands
                }
            break;
        }
    }
}
