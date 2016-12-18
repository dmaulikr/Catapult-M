using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System;

public class ClickOnlyButton : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler  {

    public UnityEvent onClick;

    public void OnPointerClick(PointerEventData eventData) {
        onClick.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        Image im = GetComponent<Image>();
        im.color = Color.gray;
    }

    public void OnPointerExit(PointerEventData eventData) {
        Image im = GetComponent<Image>();
        im.color = Color.white;
    }

    
}
