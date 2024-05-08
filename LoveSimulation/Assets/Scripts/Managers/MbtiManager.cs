using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enum;

public class MbtiManager : Singleton<MbtiManager>
{
    public MIBTIType myType { get; private set; } = MIBTIType.None;

    public MbtiManager() { }
    ~MbtiManager() { }


}
