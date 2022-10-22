using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelData))]
public class LevelEditor : Editor
{
    Texture2D currentTexture;
    VisualElement myInspector;

    public override VisualElement CreateInspectorGUI()
    {
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
            button.clickable.clicked += () =>
            {
                if(!currentTexture)
                {
                    return;
                }

                StyleBackground styleBackground = new StyleBackground
                {
                    value = new Background { texture = currentTexture }
                };

                button.style.backgroundImage = styleBackground;
            };
        });

        return myInspector;
    }
}
