using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Figure : MonoBehaviour, IFigure
{
    public Action<Role> OnActionMoveMade;

    private Role my_role;
    private Image image;
    private Canvas mainCanvas;
    private CanvasGroup canvasGroup;
    private RectTransform figureRectTransform;
    private Transform pastCellTransform;
    private Transform boardTransform;
    private float sizing;

    private void Awake()
    {
        mainCanvas = GetComponentInParent<Canvas>();
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        image = gameObject.AddComponent<Image>();
        figureRectTransform = gameObject.GetComponent<RectTransform>();
        boardTransform = GetComponentInParent<Board>().transform;
        pastCellTransform = GetComponentInParent<Transform>();
        sizing = 1.8f;
    }

    public void Appoint(FigureConfig figureConfig)
    {
        my_role = figureConfig.Role;
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
    }
    public void OnDrag(PointerEventData eventData)
    {
        figureRectTransform.anchoredPosition += eventData.delta / mainCanvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        canvasGroup.blocksRaycasts = true;
        OnActionMoveMade?.Invoke(my_role);
    }
}

