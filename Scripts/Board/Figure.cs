using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Figure : MonoBehaviour, IFigure
{
    public event Action<Figure> OnActionFigureLifted;
    public FigureConfig figureConfig {get; private set; }

    private Image image;
    private Canvas mainCanvas;
    private CanvasGroup canvasGroup;
    private RectTransform figureRectTransform;
    private float sizing = 1.8f;
    public bool isMooved { get; set; } = false;
    public bool isInGame { get; set; } = true;
    private List<Cell> MoveableСells = new List<Cell>();

    private void Awake()
    {
        mainCanvas = GetComponentInParent<Canvas>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        image = gameObject.AddComponent<Image>();
        figureRectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isInGame) return;
        canvasGroup.blocksRaycasts = false;
        OnActionFigureLifted?.Invoke(this);
        fillingListAvailableCells();
    }
    public void OnDrag(PointerEventData eventData)
    {
        figureRectTransform.anchoredPosition += eventData.delta / mainCanvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        clearAvailableCells();
        transform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
    }
    public void Appoint(FigureConfig figureConfig)
    {
        this.figureConfig = figureConfig;
        gameObject.name = figureConfig.name;
        setSprite(figureConfig.Sprite);
        figureRectTransform.localPosition = Vector3.zero;
        figureRectTransform.sizeDelta = figureRectTransform.sizeDelta * sizing;
    }

    private void setSprite(Sprite _img)
    {
        image.sprite = _img;
    }

    private void fillingListAvailableCells()
    {
        MoveableСells = RoleRules.GetAvailableCells(this);
        foreach(Cell e in MoveableСells)
        {
            e.gameObject.GetComponent<Image>().color = new Color32 (0, 255, 0, 150);
            Debug.Log($"Cell {e.transform.name} is avalible");
        }
    }

    private void clearAvailableCells()
    {
        foreach(Cell e in MoveableСells)
        {
            e.gameObject.GetComponent<Image>().color = new Color32 (255, 255, 255, 0);
        }
        MoveableСells.Clear();
    }

    public bool isCellInTheTempList(Cell cell)
    {
        foreach(Cell e in MoveableСells)
        {
            if (cell.Equals(e))
                return true;
        }
        return false;
    }
}

