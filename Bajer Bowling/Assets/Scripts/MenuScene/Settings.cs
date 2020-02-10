using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Settings : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private bool isSel;

    void Start()
    {
        isSel = false;
    }

    void Update()
    {
        if (isSel && Input.GetButtonDown("Submit"))
        {
            Debug.Log("Enter settings menu");
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        isSel = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        isSel = false;
    }
}
