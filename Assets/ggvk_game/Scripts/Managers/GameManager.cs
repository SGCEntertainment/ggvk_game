using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    public static int width = 7;
    public static int height = 7;

    Grid grid;

    [SerializeField] Sprite middleEmoji;
    [SerializeField] Sprite angryEmoji;
    [SerializeField] Sprite funEmoji;

    [Space(10)]
    [SerializeField] LevelData[] levelDatas;

    private void Awake()
    {
        CacheComponents();
    }

    private void Start()
    {
        LoadLevel(0);
    }

    void CacheComponents()
    {
        grid = FindObjectOfType<Grid>();
    }

    public Sprite GetEmoji(int id) => id switch
    {
        1 => middleEmoji,
        2 => angryEmoji,
        3 => funEmoji,

        _ => null
    };

    char[] GetLevelCharArray(int levelId)
    {
        return levelDatas[levelId].levelString.ToCharArray();
    }

    public void LoadLevel(int levelId)
    {
        char[] _level = GetLevelCharArray(levelId);
        int[,] intGrid = new int[height, width];

        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                char tmp = _level[x + y * width];
                intGrid[y, x] = (int)char.GetNumericValue(tmp);
            }
        }
        
        grid.SetEmoji(intGrid);
    }
}
