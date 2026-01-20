using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class SpinWheel : MonoBehaviour, IPointerClickHandler
{
    private SpinningWheelBehaviour spinScript;
    private float baseSpinSpeed;

    private void Awake()
    {
        spinScript = GetComponentInParent<SpinningWheelBehaviour>();
        baseSpinSpeed = spinScript.spinSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            spinScript.RequestSpin();
        }
    }

    public void OnPointerClick (PointerEventData eventData)
    {
       spinScript.RequestSpin();
    }
    
}
