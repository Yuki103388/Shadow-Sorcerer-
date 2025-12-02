using UnityEngine;

public class DiceInstance : MonoBehaviour
{
    private void Awake()
    {
        DicePuzzle.Instance.ResetDiceList();
    }
}
