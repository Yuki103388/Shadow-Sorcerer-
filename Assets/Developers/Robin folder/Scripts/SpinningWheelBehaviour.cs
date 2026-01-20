using Oculus.Interaction.Unity.Input;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

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
    public bool frozen = false;
    private bool canSpin = false;
    public float spinSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        script = GetComponent<SpinningWheelBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        SpinWheel();
    }

    public void FreezeWheel()
    {
        frozen = true;
        CheckCondition();
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
                    case ElementWheel.win: spinSpeed = 35f; RequestSpin(); break;
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

    public void RequestSpin()
    {
        if (canSpin) return;
            spinSpeed = 150f;
            canSpin = true;
    }

    private void SpinWheel()
    {
        if(!frozen && canSpin) {
            transform.Rotate(0, 0, -spinSpeed);
            spinSpeed *= .99f;
            if(spinSpeed <= .1f)
            {
                CheckCondition();
                canSpin = false;
            }
        }
    }
}
