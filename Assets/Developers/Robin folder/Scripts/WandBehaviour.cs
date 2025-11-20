using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WandBehaviour : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject _elementGem;// change this to a gem script
    [SerializeField] LayerMask _electricityLayer;
    [SerializeField] Transform _wandEndTrans;
    [SerializeField] Transform _gemSlot;

    [Header("References")]
    private RaycastHit _hit;
   

    private void Update()
    {
      
    }
    public void ElementWandResponse(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (_elementGem.tag)// change this to the gem script 
            {
                case "Electricity":
                    ElectricGemBehaviour();break;
            }
        }
    }

    private void ElectricGemBehaviour()
    {
        // checks if the raycast hits an object that is on the electricty layer and reads the name, we could also do this in the elctric gem script
        if (Physics.Raycast(_wandEndTrans.position, transform.TransformDirection(Vector3.up), out _hit, Mathf.Infinity, _electricityLayer))
            Debug.Log(_hit.collider.name); //instead of the name we could get a function to activate from that object
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gem"))
        {
            other.gameObject.transform.parent = _gemSlot;
            other.transform.position = Vector3.zero;
            _elementGem = other.gameObject;
        }
    }
}
