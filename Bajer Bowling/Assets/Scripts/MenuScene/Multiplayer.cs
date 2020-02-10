using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Multiplayer : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private bool isSel;
    [SerializeField] GameObject multiplayerMenu = default;

    void Start()
    {
        isSel = false;
    }

    void Update()
    {
        if (isSel && Input.GetButtonDown("Submit"))
        {
            Debug.Log("Enter multiplayer menu");
            GameObject.Find("MainMenu").SetActive(false);
            multiplayerMenu.SetActive(true);
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
