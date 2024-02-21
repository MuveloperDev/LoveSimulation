using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_Out : UIManagerTemplate<UIManager_Out>
{
    public override void Initialize()
    {
        canvasName = "out_";
        base.Initialize();
    }
    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        canvasName = "out_";
    }

}
