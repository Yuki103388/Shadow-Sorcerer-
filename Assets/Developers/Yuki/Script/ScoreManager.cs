using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score;
    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
       
    }

    public void AddScore(int amount)
    {
        score += amount;
        // Optionally, update UI or trigger events here
    }
}
