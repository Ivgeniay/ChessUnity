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

        if (!RoleRules.canMove(figure, transform)) return;

        if (transform.childCount == 0)
        {
            if (!figure.isMooved) figure.isMooved = true; 
            figureTransform.SetParent(transform);
            figureTransform.localPosition = Vector3.zero;
            OnActionFigureWasPlaced?.Invoke(figure);
        }

    }
}

public static class RoleRules
{
    public static bool canMove(Figure figure, Transform thisTransform)
    {
        var dir = directional(figure.transform.parent.name, thisTransform.name);
        var dist = distance(figure.transform.parent.name, thisTransform.name);

        switch (figure.figureConfig.Role)
        {
            case Role.pawn:
                return pawnRole(figure);
            case Role.Knight:
                return knightRole(figure);
        }

        return false;
    }

    private static bool pawnRole(Figure figure)
    {
        if (figure.isMooved)
        {
            
        }
        return false;
    }
    private static bool knightRole(Figure figure)
    {
        return false;
    }

    private static int distance(string cellFrom, string cellTo)
    {
        int result = default;



        return result;
    }

    //A1,A5   A1,G1   
    private static Directional directional(string cellFrom, string cellTo)
    {
        Directional dir = Directional.none;

        if (cellFrom == cellTo) dir = Directional.none;
        if (cellFrom[0] == cellTo[0] && cellFrom[1] < cellTo[1]) dir = Directional.North;


        return dir;
    }
}
