using UnityEngine;

public class ConditionCheckBehaviour : MonoBehaviour
{
    [SerializeField] LayerMask conditionMask;
    private RaycastHit hit;
    private SpinningWheelBehaviour wheelBehaviour;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        wheelBehaviour =GetComponentInParent<SpinningWheelBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        ConditionCheck();
        Debug.Log(wheelBehaviour.spinSpeed);
    }

    private void ConditionCheck()
    {
        Ray ray = new Ray(transform.position,transform.TransformDirection(transform.up));
        if (wheelBehaviour.spinSpeed <=.05f &&Physics.Raycast(ray, out hit, 15f, conditionMask))
            Debug.Log(hit.transform.name);
    }
}
