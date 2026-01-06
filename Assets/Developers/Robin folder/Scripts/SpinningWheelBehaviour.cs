using Oculus.Interaction.Unity.Input;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Collections;
using System.Collections.Generic;

public class SpinningWheelBehaviour : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private List<WheelElement> _wheelElement;
    [SerializeField] private float spinSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _transform = GameObject.Find("wheel").transform;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpinWheel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartCoroutine(SpinWheelTest());
        }
    }

    private void CheckWinOrLose()
    {

    }

    private IEnumerator SpinWheelTest()
    {
        while(true) { 
            _transform.Rotate(0, 0, -spinSpeed);
            spinSpeed = .99f * spinSpeed; 
            if(spinSpeed <= .005f)
            {
                spinSpeed = 150;
                StopAllCoroutines();
            }
            yield return null;
        }
    }
}
