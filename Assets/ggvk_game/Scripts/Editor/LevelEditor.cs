using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelData))]
public class LevelEditor : Editor
{
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
            currentTexture = middleBtn.resolvedStyle.backgroundImage.texture;
        };

        Button angryBtn = myInspector.Query<Button>("btn_angry");
        angryBtn.clickable.clicked += () =>
        {
            currentTexture = angryBtn.resolvedStyle.backgroundImage.texture;
        };

        Button funBtn = myInspector.Query<Button>("btn_fun");
        funBtn.clickable.clicked += () =>
        {
            currentTexture = funBtn.resolvedStyle.backgroundImage.texture;
        };

        var cells = myInspector.Query<Button>("cell");
        cells.ForEach((button) =>
        {
            currentTexture = levelData.cells[0,0] switch
            {
                0 => middleBtn.resolvedStyle.backgroundImage.texture,
                1 => angryBtn.resolvedStyle.backgroundImage.texture,
                2 => funBtn.resolvedStyle.backgroundImage.texture,
            };

            StyleBackground styleBackground = new StyleBackground
            {
                value = new Background { texture = currentTexture }
            };

            button.style.backgroundImage = styleBackground;

            button.clickable.clicked += () =>
            {
                if (!currentTexture)
                {
                    return;
                }

                StyleBackground styleBackground = new StyleBackground
                {
                    value = new Background { texture = currentTexture }
                };

                button.style.backgroundImage = styleBackground;
                
                if(currentTexture.name == angryBtn.resolvedStyle.backgroundImage.texture.name)
                {
                    //levelData.cell0 = 1;
                    levelData.cells[0, 0] = 1;
                }
                else if (currentTexture.name == middleBtn.resolvedStyle.backgroundImage.texture.name)
                {
                    //levelData.cell0 = 0;
                    levelData.cells[0, 0] = 0;
                }
                else if (currentTexture.name == funBtn.resolvedStyle.backgroundImage.texture.name)
                {
                    //levelData.cell0 = 2;
                    levelData.cells[0, 0] = 2;
                }
            };
        });

        return myInspector;
    }
}
