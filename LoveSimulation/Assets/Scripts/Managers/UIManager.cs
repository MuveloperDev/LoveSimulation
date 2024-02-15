using Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public bool isExistance { get; private set; }

    public UIManager() { }
    ~UIManager() { }
}
