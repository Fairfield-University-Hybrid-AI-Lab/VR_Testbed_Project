using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrailMakingSelectableHover : MonoBehaviour, IPointerEnterHandler
{
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = this.transform.GetComponent<Button>();
    }

    //When pointer hover the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        //check if the button interactable
        if (button.IsInteractable())
        {
            button.onClick.Invoke();
        }
    }
}
