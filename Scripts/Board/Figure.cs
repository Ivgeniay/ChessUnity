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

    private void Awake()
    {
        mainCanvas = GetComponentInParent<Canvas>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        image = gameObject.AddComponent<Image>();
        figureRectTransform = gameObject.GetComponent<RectTransform>();
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        OnActionFigureLifted?.Invoke(this);
    }
    public void OnDrag(PointerEventData eventData)
    {
        figureRectTransform.anchoredPosition += eventData.delta / mainCanvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
    }
}

