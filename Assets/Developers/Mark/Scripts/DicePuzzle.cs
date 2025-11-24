using UnityEngine;
using TMPro;

public class DicePuzzle : MonoBehaviour
{
    [Header("References")]
    private GameObject[] diceArray;
    private Rigidbody[] diceRigidbodies;
    [SerializeField] private TextMeshPro counterText;

    [Header("Settings")]
    private int diceCount;
    private bool diceCountChecked = true;
    private bool puzzleComplete = false;

    private void Start()
    {
        diceArray = GameObject.FindGameObjectsWithTag("Dice");
        diceRigidbodies = new Rigidbody[diceArray.Length];
        for (int i = 0; i < diceArray.Length; i++)
        {
            diceRigidbodies[i] = diceArray[i].GetComponent<Rigidbody>();
        }
    }

    private void Update()
    {

        for (int i = 0; i < diceRigidbodies.Length; i++)
        {
            if (diceRigidbodies[i].linearVelocity.magnitude > 0.01f)
            {
                diceCountChecked = false;
                return;
            }
            diceCountChecked = true;
        }

        if (diceCountChecked && !puzzleComplete)
        {
            diceCount = 0;
            for (int i = 0; i < diceArray.Length; i++)
            {
                int diceValue = GetDiceValue(diceArray[i]);
                diceCount += diceValue;
            }
            counterText.text = diceCount.ToString();
        }
    }

    private int GetDiceValue(GameObject dice)
    {
        Vector3 faceUp = dice.transform.up;

        if (Vector3.Dot(faceUp, Vector3.up) > 0.8f)
            return 1;
        else if (Vector3.Dot(faceUp, Vector3.down) > 0.8f)
            return 6;
        else if (Vector3.Dot(faceUp, Vector3.forward) > 0.8f)
            return 5;
        else if (Vector3.Dot(faceUp, Vector3.back) > 0.8f)
            return 2;
        else if (Vector3.Dot(faceUp, Vector3.right) > 0.8f)
            return 3;
        else if (Vector3.Dot(faceUp, Vector3.left) > 0.8f)
            return 4;
        else
            return 0; // Error case
    }
}
