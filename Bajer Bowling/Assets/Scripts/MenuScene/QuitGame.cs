using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuitGame : MonoBehaviour, ISelectHandler, IDeselectHandler
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
            Application.Quit();
            Debug.Log("Quit");
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
