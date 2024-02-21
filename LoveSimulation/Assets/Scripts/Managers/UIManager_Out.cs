using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_Out : UIManagerTemplate<UIManager_Out>
{
    public override async void Initialize()
    {
        canvasName = "out_";
        base.Initialize();

        await CreateUIs();
    }

    private async UniTask CreateUIs()
    {
        var pagingSys = await CreateUI<PagingSystem>("Assets/Resources/Prefabs/DialogBox.prefab", Enum.UILayer.Frequent, Enum.ResourceScope.Outgame);
        
    }


}
