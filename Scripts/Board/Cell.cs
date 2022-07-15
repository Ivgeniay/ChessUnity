using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IDropHandler
{
    public event Action OnActionMoveMade;

    public void OnDrop(PointerEventData eventData)
    {
        var figure = eventData.pointerDrag.transform;
        Figure figureScr = figure.GetComponent<Figure>();

        if (transform.childCount == 0)
        {
            figure.SetParent(transform);
            figure.localPosition = Vector3.zero;
        }

    }
}
