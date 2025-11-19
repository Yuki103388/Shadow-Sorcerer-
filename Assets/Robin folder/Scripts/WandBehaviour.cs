using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WandBehaviour : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject _elementGem;// change this to a gem script
    [SerializeField] LayerMask _electricityLayer;

    [Header("References")]
    private RaycastHit _hit;
    [SerializeField] Transform _wandEndTrans;
    public void ElementWandResponse()
    {
       switch (_elementGem.tag)// change this to the gem script 
       {
         case "Electricity":if(Physics.Raycast(_wandEndTrans.position, transform.TransformDirection(Vector3.up), out _hit, Mathf.Infinity, _electricityLayer)) Debug.Log(_hit.collider.name); break;
       }
    }

    private void Update()
    {
        //debug raycast, to visualise the raycast
        var dir = transform.TransformDirection(Vector3.up) * 10;
        Debug.DrawRay(_wandEndTrans.position,dir, Color.red);
    }
}
