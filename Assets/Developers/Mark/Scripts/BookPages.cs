using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPages : MonoBehaviour
{
    private bool canTurnPage = true;

    private List<GameObject> pages = new();

    public void TurnPageLeft()
    {
        if (canTurnPage)
        {
            StartCoroutine(TurnPageCoroutine(true));
        }
    }
    public void TurnPageRight()
    {
        if (canTurnPage)
        {
            StartCoroutine(TurnPageCoroutine(false));
        }
    }

    private IEnumerator TurnPageCoroutine(bool isLeft)
    {
        float _XRotation = 0;
        canTurnPage = false;
        while (_XRotation < 180)
        {
            _XRotation += 1f;

            if (isLeft)
                transform.localRotation = Quaternion.Euler(_XRotation, 0, 0);
            else
                transform.localRotation = Quaternion.Euler(180 - _XRotation, 0, 0);

            yield return null;
        }

        if (isLeft)
            transform.localRotation = Quaternion.Euler(180, 0, 0);
        else
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        canTurnPage = true;
    }
}
