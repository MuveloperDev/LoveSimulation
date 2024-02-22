using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_Out : UIManagerTemplate<UIManager_Out>
{
    public void Initialize()
    { 
    
    }
    protected override async void InitializeTemplate()
    {
        canvasName = "out_";
        base.InitializeTemplate();

        await CreateUIs();
    }

    private async UniTask CreateUIs()
    {
        var title = await CreateUI<TitleManager>("Assets/Resources/Prefabs/Title.prefab", Enum.UILayer.Frequent, Enum.ResourceScope.Outgame);
        var pagingSys = await CreateUI<PagingSystem>("Assets/Resources/Prefabs/DialogBox.prefab", Enum.UILayer.Frequent, Enum.ResourceScope.Outgame);
        pagingSys.Hide();
        
    }


}
