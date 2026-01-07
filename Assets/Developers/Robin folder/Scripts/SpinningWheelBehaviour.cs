using Oculus.Interaction.Unity.Input;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Collections;
using System.Collections.Generic;

public class SpinningWheelBehaviour : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Transform _conditionCheckTrans;
    [SerializeField] LayerMask _conditionLayer;
    [SerializeField] List<WheelElement> _wheelElements;
    [Header("Variables")]
    private bool frozen = false;
    public float spinSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpinWheel(InputAction.CallbackContext context)
    {
        if (context.performed && !frozen)
        {
            StartCoroutine(SpinWheelTest());
        }
    }

    public void FreezeWheel()
    {
        StopAllCoroutines();
        frozen = true;
    }

    public void UnfreezeWheel()
    {
        frozen = false;
    }

    private void CheckCondition()
    {
        RaycastHit hit;
        Ray ray = new Ray(_conditionCheckTrans.position,_conditionCheckTrans.TransformDirection(Vector3.right));
        if(Physics.Raycast(ray,out hit, 5f, _conditionLayer))
        {
            switch (hit.transform.parent.GetComponent<WheelElement>().wheelElement)
            {
                case ElementWheel.lose: Debug.Log("play a sfx or something loser"); break;
                case ElementWheel.win: spinSpeed = 35f; StartCoroutine(SpinWheelTest()); break;
            }
        }
    }

    private IEnumerator SpinWheelTest()
    {
        while(true) {
            transform.Rotate(0, 0, -spinSpeed);
            spinSpeed = .99f * spinSpeed; 
            if(spinSpeed <= .05f)
            {
                CheckCondition();
                spinSpeed = 150;
                StopAllCoroutines();
            }
            yield return null;
        }
    }
}
