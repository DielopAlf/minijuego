using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MovBoton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 defaultScale;

    void Start()
    {
        defaultScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, defaultScale * 1.1f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, defaultScale, 0.2f);
    }
}

