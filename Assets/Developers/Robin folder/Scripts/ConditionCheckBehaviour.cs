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
    }

    private void ConditionCheck()
    {
        Ray ray = new Ray(transform.position,transform.TransformDirection(Vector3.up) * 10);
        if (wheelBehaviour.spinSpeed <=.05f &&Physics.Raycast(ray, out hit, Mathf.Infinity, conditionMask))
            Debug.Log(hit.transform.name);
    }
}
