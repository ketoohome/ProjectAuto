using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameCommon;
using TOOL;

namespace GameCommon{
	/// <summary>
	/// UI 类型ID
	/// </summary>
	public enum UIType : int
	{
		UIMenu = 0,     // 标题界面
		UIPlay = 1,     // 游戏界面
		UILoading = 2,  // Loading界面
		// ...
	}
}

namespace GameUI{
	
    /// <summary>
    /// UI注册类，所有UI控件必须在这里注册才能被使用
    /// </summary>
	public class UI_GameRoot : IUIRoot{}
}