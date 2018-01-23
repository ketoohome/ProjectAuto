using UnityEngine;
using System.Collections;
using GameCommon;
using TOOL;

namespace GameLogic{
	public class L_System_Menu : L_System {
        
        /// <summary>
        /// 主界面选择的缓存数据
        /// </summary>
        public static L_Data MenuData { 
            get {
                L_Data data = L_DataPool.Instance.FindChild("MenuData");
                if (data == null) data = L_DataPool.Instance.CreatChildData("MenuData",null);
                return data; 
            }
        }

		public override void Start(){
            // 清空缓存数据
            MenuData.ClearChildren();

			// 注册监听函数
            EventMachine.Register(EventID.Event_UI_Menu_Play, OnPlay);
            EventMachine.Register(EventID.Event_UI_Menu_TwoPlay, OnTwoPlay);
            EventMachine.Register(EventID.Event_UI_Menu_Multiplayer, OnMultiplayer);
            // 创建一个UI
			EventMachine.SendEvent(EventID.Event_UI_Create,UIType.UIMenu);
		} 

		public override void End(){
			// 移除一个UI
			EventMachine.SendEvent(EventID.Event_UI_Delete,UIType.UIMenu);
            EventMachine.SendEvent(EventID.Event_UI_Create, UIType.UILoading);
			// 注销监听函数
            EventMachine.Unregister(EventID.Event_UI_Menu_Play, OnPlay);
            EventMachine.Unregister(EventID.Event_UI_Menu_TwoPlay, OnTwoPlay);
            EventMachine.Unregister(EventID.Event_UI_Menu_Multiplayer, OnMultiplayer);
        }

		public override void CustomUpdate () {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EventMachine.SendEvent(EventID.Event_UI_Create, UIType.UIRank);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                L_RankManager.Instance.ClearData("TestRank");
            }
        }
        /// <summary>
        /// 开始游戏
        /// </summary>
        /// <param name="args"></param>
		void OnPlay(params object[] args)
        {
            L_Data data = MenuData.CreatChildData("PlayType", "Singleplayer");
            L_Root.ChangeState(GameState.GS_Play);
		}

        /// <summary>
        /// 开始双人游戏
        /// </summary>
        /// <param name="args"></param>
        void OnTwoPlay(params object[] args)
        {
            L_Data data = MenuData.CreatChildData("PlayType", "TowPlayers");
            L_Root.ChangeState(GameState.GS_Play);
        }

        /// <summary>
        /// 开始多人游戏（查找）
        /// </summary>
        /// <param name="args"></param>
        void OnMultiplayer(params object[] args)
        {
            L_Data data = MenuData.CreatChildData("PlayType", "Mulitiplayer");
            L_Root.ChangeState(GameState.GS_Play);
        }
	}
}