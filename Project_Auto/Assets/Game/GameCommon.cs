namespace GameCommon
{
	/// <summary>
	/// UI 类型ID
	/// </summary>
	public enum UIType : int
	{
		UIMenu = 0,     // 标题界面
		UIPlay = 1,     // 游戏界面
		UILoading = 2,  // Loading界面
        UIRank = 3,     // 排行榜界面
		// ...
	}

    /// <summary>
    /// 游戏事件ID
    /// </summary>
    public enum EventID
    {
        /********************************公用事件类型（勿改）*****************************************/
        // 自定义事件
        Event_Custom = 0,				// 自定义事件，第一个参数可做为标识

        // 基础Game事件
        Event_GameLogicStart = 101,     //游戏逻辑初始化完成，游戏启动
        Event_GameLogicStop,	        // 游戏逻辑停止，游戏结束

        // 基础UI事件
        Event_UI_Create = 201,			// 创建UI
        Event_UI_Delete,				// 删除UI
        Event_UI_DeleteAll,				// 删除所有UI

        // 基础其他事件
        Event_Time_ScaleChange = 401,	// 时间间隔被改变
        /***********************************************************************************************/

        // 游戏逻辑事件（1000~2000）
		Event_Loading = 1000,           // Loading 进度更新
        // ...

        // UI事件（2001~3000）
        Event_UI_Menu_Play = 2001,              // 开始单机游戏
        Event_UI_Menu_TwoPlay,                  // 开始双人游戏
        Event_UI_Menu_Multiplayer,              // 开始多人游戏

        Event_UI_Play_Back = 2101,              // 返回标题界面
        // ...
        
        // 传达给UI的事件（3001~4000）
        // ...

        // 表现（5001~6000）
        Event_Effect_CameraVibration = 5001,   	// 摄像机震动
		Event_Effect_CameraAnimation = 5002,	// 摄像机动画
    }
}