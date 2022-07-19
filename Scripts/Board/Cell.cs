using System.Transactions;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IDropHandler
{
    public event Action<Figure> OnActionFigureWasPlaced;

    public void OnDrop(PointerEventData eventData)
    {
        var figureTransform = eventData.pointerDrag.transform;
        Figure figure = figureTransform.GetComponent<Figure>();

        //if (!RoleRules.canMove(figure, figureTransform.parent.name, transform.name)) return;

        if (transform.childCount == 0)
        {
            if (!figure.isMooved) figure.isMooved = true; 
            figureTransform.SetParent(transform);
            figureTransform.localPosition = Vector3.zero;
            OnActionFigureWasPlaced?.Invoke(figure);
        }

    }
}
