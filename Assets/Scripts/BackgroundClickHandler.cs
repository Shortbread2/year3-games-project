using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackgroundClickHandler : MonoBehaviour, IPointerClickHandler
{
    public InteractableObject interactableObject;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Check if the click is outside the puzzle panel
        interactableObject.OnBackgroundClick();
    }
}
