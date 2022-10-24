using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    [SerializeField] GameObject gameUIGo;
    [SerializeField] GameObject menuGo;
    [SerializeField] GameObject winPopupGO;
    
    private void Start()
    {
        menuGo.SetActive(true);
        gameUIGo.SetActive(false);
        winPopupGO.SetActive(false);

        GameManager.OnLevelCompleted += () =>
        {
            winPopupGO.SetActive(true);
        };
    }

    public void OpenWindow(int id)
    {
        if(id == 0)
        {
            winPopupGO.SetActive(false);
            gameUIGo.SetActive(false);
            menuGo.SetActive(true);
        }
        else if(id == 1)
        {
            menuGo.SetActive(false);
            gameUIGo.SetActive(true);
        }
    }
}
