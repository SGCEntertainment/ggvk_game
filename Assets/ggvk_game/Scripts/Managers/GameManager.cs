using UnityEngine;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

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

    Grid grid;
    Cell lastColl;
    LineRenderer line;

    public static int width = 7;
    public static int height = 7;

    [SerializeField] Sprite middleEmoji;
    [SerializeField] Sprite angryEmoji;
    [SerializeField] Sprite funEmoji;

    bool MousePressed
    {
        get => Input.GetMouseButton(0);
    }

    [HideInInspector]
    public bool SequenceStarted;

    [Space(10)]
    [SerializeField] LevelData[] levelDatas;
    List<Cell> activeCells = new List<Cell>();

    private void Awake()
    {
        CacheComponents();
    }

    private void Start()
    {
        LoadLevel(0);
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            //SequenceStarted = false;
            //line.positionCount = 0;
            //activeCells.Clear();
        }

        UpdateLine();
    }

    void CacheComponents()
    {
        grid = FindObjectOfType<Grid>();
        line = FindObjectOfType<LineRenderer>();
    }

    void UpdateLine()
    {
        line.positionCount = activeCells.Count;
        for(int i = 0; i < activeCells.Count; i++)
        {
            line.SetPosition(i, activeCells[i].transform.position);
        }
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

    public void AddCell(Cell cell)
    {
        if (activeCells.Count == 0 && cell.Icon == funEmoji)
        {
            SequenceStarted = true;
        }

        bool isAngry = cell.Icon == angryEmoji;
        bool lastAngry = lastColl != null && lastColl.Icon == angryEmoji;
        bool canClose = activeCells.Count > 2 && cell.Icon == funEmoji;
        if (!canClose && activeCells.Contains(cell) || isAngry || lastAngry || !SequenceStarted)
        {
            lastColl = cell;
            return;
        }

        activeCells.Add(cell);
    }
}
