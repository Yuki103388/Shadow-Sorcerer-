using UnityEngine;
using TMPro;
using System.Collections;

public class DicePuzzle : MonoBehaviour
{
    #region Singleton

    private static DicePuzzle m_Instance;
    public static DicePuzzle Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindFirstObjectByType<DicePuzzle>();
                if (m_Instance == null)
                {
                    Debug.LogError("No instance of DicePuzzle found in the scene.");
                }
            }

            return m_Instance;
        }
    }

    #endregion

    [Header("References")]
    private GameObject[] diceArray;
    private Rigidbody[] diceRigidbodies;
    [SerializeField] private TextMeshPro counterText;

    [Header("Settings")]
    private int diceCount;
    private bool diceCountChecked = true;
    private bool puzzleComplete = false;

    private void Update()
    {

        for (int i = 0; i < diceRigidbodies.Length; i++)
        {
            if (diceRigidbodies[i].linearVelocity.magnitude > 0.01f || diceRigidbodies[i].isKinematic == true)
            {
                diceCountChecked = false;
                counterText.text = "Rolling...";
                return;
            }
            StartCoroutine(SetDiceCountCheckedAfterDelay(0.5f));
        }

        if (diceCountChecked && !puzzleComplete)
        {
            diceCount = 0;
            for (int i = 0; i < diceArray.Length; i++)
            {
                int diceValue = GetDiceValue(diceArray[i]);
                diceCount += diceValue;
            }
            counterText.text = diceCount.ToString() + " / 11";
        }

        if (diceCount >= 11 && !puzzleComplete)
        {
            puzzleComplete = true;
            Debug.Log("Puzzle Complete!");
            // Additional puzzle completion logic here
        }
    }

    public void ResetDiceList()
    {
        diceArray = GameObject.FindGameObjectsWithTag("Dice");
        diceRigidbodies = new Rigidbody[diceArray.Length];
        for (int i = 0; i < diceArray.Length; i++)
        {
            diceRigidbodies[i] = diceArray[i].GetComponent<Rigidbody>();
        }
    }

    private IEnumerator SetDiceCountCheckedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        diceCountChecked = true;
    }

    private int GetDiceValue(GameObject dice)
    {
        GameObject HighestFace = null;
        float highestY = float.NegativeInfinity;

        for (int i = 0; i < dice.transform.childCount; i++)
        {
            GameObject face = dice.transform.GetChild(i).gameObject;
            if (face != null && face.transform.position.y > highestY)
            {
                highestY = face.transform.position.y;
                HighestFace = face;
            }
        }

        return int.Parse(HighestFace.name);
    }
}
