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
    [SerializeField] Transform winObjTrans;
    [SerializeField] LayerMask _conditionLayer;
    [SerializeField] List<WheelElement> _wheelElements;
    [SerializeField] GameObject winObject;
    private SpinningWheelBehaviour script;

    [Header("Variables")]
    private bool frozen = false;
    public float spinSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        script = GetComponent<SpinningWheelBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpinWheelInput(InputAction.CallbackContext context)
    {
        if (context.performed && !frozen)
        {
            StartCoroutine(SpinWheel());
        }
    }

    public void FreezeWheel()
    {
        spinSpeed = 0;
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
            if (!frozen)
            {
                switch (hit.transform.parent.GetComponent<WheelElement>().wheelElement)
                {
                    case ElementWheel.lose: Debug.Log("play a sfx or something loser"); break;
                    case ElementWheel.win: spinSpeed = 35f; StartCoroutine(SpinWheel()); break;
                }
            }
            else if(frozen)
            {
                switch (hit.transform.parent.GetComponent<WheelElement>().wheelElement)
                {
                    case ElementWheel.lose: Debug.Log("play a sfx or something loser"); break;
                    case ElementWheel.win: Debug.Log("win");Instantiate(winObject,winObjTrans);script.enabled = false; break;
                }
            } 
        }
    }

    public IEnumerator SpinWheel()
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
