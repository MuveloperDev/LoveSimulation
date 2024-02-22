using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_In : UIManagerTemplate<UIManager_In>
{
    public void Initialize()
    {

    }
    protected override void InitializeTemplate()
    {
        canvasName = "in_";
        base.InitializeTemplate();
    }
}

