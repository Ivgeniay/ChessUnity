using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Figure : MonoBehaviour, IFigure
{
    private Role my_role;
    private Image image;
    private RectTransform rectTransform;
    private float sizing;

    private void Awake()
    {
        image = gameObject.AddComponent<Image>();
        rectTransform = gameObject.GetComponent<RectTransform>();
        sizing = 1.8f;
    }

    public void Appoint(FigureConfig figureConfig)
    {
        my_role = figureConfig.Role;
        gameObject.name = figureConfig.name;
        setSprite(figureConfig.Sprite);
        rectTransform.localPosition = Vector3.zero;
        rectTransform.sizeDelta = rectTransform.sizeDelta * sizing;
    }

    private void setSprite(Sprite _img)
    {
        image.sprite = _img;
    }




}

