using UnityEngine;

public class Updraft : MonoBehaviour
{
    [SerializeField] private float updraftBuildUpSpeed = 7f;
    [SerializeField] private float maxUpdraft = 50f;

    private bool updraftOn;
    private bool updraftReleased;
    private float updraftBuildup;

    private void Update()
    {
        if(updraftOn && !updraftReleased)
        {
            updraftBuildup += updraftBuildUpSpeed*Time.deltaTime;
            Debug.Log(updraftBuildup);
        }

        if(updraftBuildup > maxUpdraft)
        {
            updraftBuildup = maxUpdraft;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Rigidbody body = other.gameObject.GetComponent<Rigidbody>();
        if(body != null && updraftReleased)
        {
            body.AddForce(Vector3.up * updraftBuildup);
            Debug.Log(updraftBuildup);
        }
    }

    [ContextMenu("Toggle Updraft Buildup")]
    public void StartUpdraft()
    {
        updraftOn = true;
    }

    [ContextMenu("Toggle Updraft")]
    public void ReleaseUpdraft()
    {
        updraftReleased = !updraftReleased;
    }
}
