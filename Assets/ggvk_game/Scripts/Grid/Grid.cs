using UnityEngine;

public class Grid : MonoBehaviour
{
    int width = GameManager.width;
    int height = GameManager.height;

    Cell[,] cells;

    public void InitCells()
    {
        cells = new Cell[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                cells[y, x] = transform.GetChild(x + y * width).GetComponent<Cell>();
            }
        }
    }

    public void SetEmoji(int[,] grid)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                cells[y, x].Icon = GameManager.Instance.GetEmoji(grid[y,x]);
            }
        }
    }
}
