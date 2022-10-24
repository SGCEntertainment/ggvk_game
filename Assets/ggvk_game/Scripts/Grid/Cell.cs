using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Sprite Icon
    {
        get => transform.GetChild(0).GetComponent<Image>().sprite;
        set => transform.GetChild(0).GetComponent<Image>().sprite = value;
    }
}
