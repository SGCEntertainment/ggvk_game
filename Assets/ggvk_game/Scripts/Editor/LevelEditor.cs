using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;
using System.Linq;

[CustomEditor(typeof(LevelData))]
public class LevelEditor : Editor
{
    int selectedIndex;

    Texture2D currentTexture;
    VisualElement myInspector;

    LevelData levelData;

    public override VisualElement CreateInspectorGUI()
    {
        levelData = target as LevelData;

        myInspector = new VisualElement();
        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/ggvk_game/Scripts/Editor/LevelInspector.uxml");
        visualTree.CloneTree(myInspector);

        Button middleBtn = myInspector.Query<Button>("btn_middle");
        middleBtn.clickable.clicked += () =>
        {
            selectedIndex = 1;
            currentTexture = middleBtn.resolvedStyle.backgroundImage.texture;
        };

        Button angryBtn = myInspector.Query<Button>("btn_angry");
        angryBtn.clickable.clicked += () =>
        {
            selectedIndex = 2;
            currentTexture = angryBtn.resolvedStyle.backgroundImage.texture;
        };

        Button funBtn = myInspector.Query<Button>("btn_fun");
        funBtn.clickable.clicked += () =>
        {
            selectedIndex = 3;
            currentTexture = funBtn.resolvedStyle.backgroundImage.texture;
        };

        Button eraserBtn = myInspector.Query<Button>("btn_eraser");
        eraserBtn.clickable.clicked += () =>
        {
            selectedIndex = 0;
            currentTexture = null;
        };

        char[] grid = levelData.levelString.ToCharArray();

        VisualElement gridElement = myInspector.Query<VisualElement>("grid");
        VisualElement[] rootBtnElement = gridElement.Children().ToArray();
        
        for(int i = 0; i < rootBtnElement.Length; i++)
        {
            char currentEmojiId = grid[i];
            Button cellBtn = rootBtnElement[i][0] as Button;

            Background background = new Background
            {
                texture = currentEmojiId switch
                {
                    '1' => middleBtn.resolvedStyle.backgroundImage.texture,
                    '2' => angryBtn.resolvedStyle.backgroundImage.texture,
                    '3' => funBtn.resolvedStyle.backgroundImage.texture,

                    _ => null
                }
            };

            cellBtn.style.backgroundImage = new StyleBackground
            { 
                value = background
            };

            cellBtn.clickable.clicked += () =>
            {
                background = new Background
                {
                    texture = currentTexture
                };

                cellBtn.style.backgroundImage = new StyleBackground
                {
                    value = background
                };

                int id = gridElement.IndexOf(cellBtn.parent);
                grid[id] = selectedIndex.ToString()[0];
                levelData.levelString = new string(grid); 
            };
        }

        EditorUtility.SetDirty(target);
        return myInspector;
    }
}