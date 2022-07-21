using System.Transactions;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IDropHandler
{
    public event Action<Figure> OnActionFigureWasPlaced;
    private const string OUT_OF_GAME_WHITE_TAG = "OutOfGameWhite";
    private const string OUT_OF_GAME_BLACK_TAG = "OutOfGameBlack";



    public void OnDrop(PointerEventData eventData)
    {
        var figureTransform = eventData.pointerDrag.transform;
        Figure figure = figureTransform.GetComponent<Figure>();

        //if (!RoleRules.canMove(figure, figureTransform.parent.name, transform.name)) return;
        if (figure.isCellInTheTempList(this))
        {
            if (transform.childCount > 0)
            {
                try
                {
                    Transform go = transform.GetChild(0);
                    Figure _figure = go.GetComponent<Figure>();
                    if (_figure.figureConfig.Color == ColorList.White)
                        go.SetParent(GameObject.FindWithTag(OUT_OF_GAME_BLACK_TAG).transform);
                    else if (_figure.figureConfig.Color == ColorList.Black)
                        go.SetParent(GameObject.FindWithTag(OUT_OF_GAME_WHITE_TAG).transform);
                    _figure.isInGame = false;
                }
                catch{}
            }

            if (!figure.isMooved) figure.isMooved = true; 
            figureTransform.SetParent(transform);
            figureTransform.localPosition = Vector3.zero;
            OnActionFigureWasPlaced?.Invoke(figure);
        }

    }
}
