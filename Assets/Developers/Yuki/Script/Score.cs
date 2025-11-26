using UnityEngine;

public class Score : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            ScoreManager.Instance.AddScore(10);
            Destroy(gameObject);
            Debug.Log("Score increased by 10");
        }
    }
}
