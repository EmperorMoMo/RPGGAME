using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ButtonPack : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public GameObject image;

    public void OnPointerEnter(PointerEventData eventData) {
        image.SetActive(true);
        image.GetComponent<Image>().raycastTarget = false;
    }

    public void OnPointerExit(PointerEventData eventData) {
        image.SetActive(false);
    }
}
