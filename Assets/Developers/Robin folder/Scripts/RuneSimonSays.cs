using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class RuneSimonSays : MonoBehaviour,IPointerDownHandler
{
    [SerializeField] Renderer[] runes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       
    }

    private IEnumerator SimonSaysBehaviour()
    {
        WaitForSeconds wait = new WaitForSeconds(2);
        Debug.Log("start sequence");
        Debug.Log(runes.Length);
        runes[0].material.color = Color.white;
        yield return wait;
    }

   public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            StartCoroutine(SimonSaysBehaviour());
        }
    }
}
