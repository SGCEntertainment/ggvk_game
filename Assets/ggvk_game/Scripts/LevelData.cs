using UnityEngine;

[CreateAssetMenu(menuName = "Level Editor/Create new Level Data")]
public class LevelData : ScriptableObject
{
    public int cell0;
    public int[,] cells = new int[3, 3];
}
