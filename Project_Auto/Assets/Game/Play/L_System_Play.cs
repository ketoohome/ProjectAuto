using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameCommon;
using TOOL;

namespace GameLogic{
	public class L_System_Play : L_System {

		public override void Start(){

            // 4秒后关闭loading
			ClockMachine.It.CreateClock(2, () => { EventMachine.SendEvent(EventID.Event_Loading,1.0f); });

			// 创建一个UI
			EventMachine.SendEvent(EventID.Event_UI_Create,UIType.UIPlay);

			// 注册事件
            EventMachine.Register(EventID.Event_UI_Play_Back, OnPlayBack);
			// ...

            // 创建游戏规则
            string playType = L_DataPool.Instance.FindChild("MenuData","PlayType").GetValue<string>();
            if (playType == "Singleplayer")
            {
                JudgeCreater.CreateJudge<L_Judge_Single>();
            }
            if (playType == "TowPlayers")
            {
                JudgeCreater.CreateJudge<L_Judge_Towplay>();
            }
            else if (playType == "Mulitiplayer")
            {
                //创建网络管理器并创建用户
                JudgeCreater.CreateJudge<L_Judge_Network>();
            }
		} 

		public override void End() {
            // 移除一个UI
            EventMachine.SendEvent(EventID.Event_UI_Delete, UIType.UIPlay);

			// 注销事件
            EventMachine.Unregister(EventID.Event_UI_Play_Back, OnPlayBack);
			// ...
		}

		public override void CustomUpdate () {
            L_ActorManager.It.CustomUpdate();
            L_PlayerManager.It.CustomUpdate();
        }

        /// <summary>
        /// 返回主界面
        /// </summary>
        void OnPlayBack(params object[] args) {
            L_Root.ChangeState(GameState.GS_Menu);
        }
	}
}