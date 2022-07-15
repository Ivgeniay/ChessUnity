using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IDropHandler
{
    

    public void OnDrop(PointerEventData eventData)
    {
        var figure = eventData.pointerDrag.transform;

        if (transform.childCount == 0)
        {
            figure.SetParent(transform);
            figure.localPosition = Vector3.zero;
        }

    }
}
