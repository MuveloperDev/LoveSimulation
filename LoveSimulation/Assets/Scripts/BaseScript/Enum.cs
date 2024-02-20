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
		Popup
	}

    public enum ResourceScope
    {
        Global,
        Ingame,
        Outgame
    }

	public enum Scenes
	{
		TitleScene = 0,
		InGameScene
	}
}

