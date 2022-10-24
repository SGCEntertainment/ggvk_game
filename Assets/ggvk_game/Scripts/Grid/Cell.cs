using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
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
}
