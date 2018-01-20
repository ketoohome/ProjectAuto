using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOOL;

namespace GameLogic
{
    /// <summary>
    /// 游戏规则
    /// </summary>
    public class L_Judge_Towplay : L_Judge_Single{

        /// <summary>
        /// 创建用户
        /// </summary>
        protected override void CreatePlayer() {
            L_PlayerManager.It.CreatePlayer<L_Player_User>();
            L_PlayerManager.It.CreatePlayer<L_Player_User>();
        }
    }
}