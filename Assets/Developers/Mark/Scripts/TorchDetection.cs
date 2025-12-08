using UnityEngine;

public class TorchDetection : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject torchLight;

    void Start()
    {
        if (torchLight == null)
        {
            Debug.LogError("TorchDetection: Torch Light reference is not set.");
        }
        else
        {
            torchLight.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            torchLight.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            torchLight.SetActive(false);
        }
    }
}
