using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOOL;

namespace GameLogic
{
    /// <summary>
    /// 系统类型
    /// </summary>
    public enum SystemType
    {
        ST_Menu = 1,			// 主界面系统
        ST_Play = 2,			// 游戏系统
    }

    /// <summary>
    /// 游戏系统注册器
    /// </summary>
    public class L_SystemRegister : L_SystemManager
    {
        /// <summary>
        /// 所有系统在这里注册
        /// </summary>
        protected override void RegisterSystems()
        {
            IFactory<L_System>.Register<L_System_Menu>((int)SystemType.ST_Menu); // 主界面系统
            IFactory<L_System>.Register<L_System_Play>((int)SystemType.ST_Play); // 游戏系统
            //...
        }
    }
}
