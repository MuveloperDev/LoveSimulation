using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enum
{
	public enum UILayer
	{
		None = 0,
		Static,
		Frequent,
		Popup,
		Max
	}

    public enum ResourceScope
    {
        None = 0,
        Global,
        Ingame,
        Outgame,
        Max
    }

	public enum Scenes
	{
        None = 0,
        TitleScene = 0,
		InGameScene,
        Max
    }
}

