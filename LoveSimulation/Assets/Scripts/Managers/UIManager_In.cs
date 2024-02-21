using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_In : UIManagerTemplate<UIManager_In>
{
    public override void Initialize()
    {
        canvasName = "in_";
        base.Initialize();
    }
    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        canvasName = "in_";
    }

}

