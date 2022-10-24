using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public Sprite Icon
    {
        get => transform.GetChild(0).GetComponent<Image>().sprite;

        set
        {
            Image img = transform.GetChild(0).GetComponent<Image>();

            img.enabled = value != null;
            img.sprite = value;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance.AddCell(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.Instance.AddCell(this);
    }
}
