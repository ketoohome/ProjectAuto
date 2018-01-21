using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using GameCommon;
using TOOL;

namespace GameLogic
{
    /// <summary>
    /// 游戏主逻辑实体，用于注册游戏状态
    /// 游戏状态添加步骤：
    ///     1. GameState添加状态类型GS_XX
    ///     2. 创建状态类L_GameStateXX:State<L_Root>
    ///     3. L_GameRoot的RegisterStates函数中添加状态
    /// </summary>
    public class L_GameRoot : L_Root
    {
        /// <summary>
        /// 注册游戏所有状态
        /// </summary>
        protected override void RegisterStates()
        {
            m_stateMachine.Add(GameState.GS_Initialize, new L_GameStateInitialize());	// 初始化状态
            m_stateMachine.Add(GameState.GS_Menu, new L_GameStateMenu());				// Logo状态
            m_stateMachine.Add(GameState.GS_Play, new L_GameStatePlay());			    // 版本更新状态
            //...

            m_stateMachine.ChangeState(GameState.GS_Initialize);                    // 初始化第一个状态
        }

		/// <summary>
		/// Loads the scene.
		/// </summary>
		protected IEnumerator LoadAsynScene(string name){
			AsyncOperation asynLoad = SceneManager.LoadSceneAsync (name);
			while (!asynLoad.isDone) {
				EventMachine.SendEvent (EventID.Event_Loading,asynLoad.progress);
				yield return null;
			}
			EventMachine.SendEvent (EventID.Event_Loading,1.0f);
		}
    }

    /// <summary>
    /// 游戏逻辑状态枚举
    /// </summary>
    public enum GameState
    {
        GS_Initialize = 0,	// 游戏初始状态
        GS_Menu = 1,        // 游戏主界面状态
        GS_Play = 2,        // 游戏状态
    }

    /// <summary>
    /// 游戏初始化状态
    /// </summary>
    public class L_GameStateInitialize : IState<L_Root>
    {
        AsyncOperation m_Asyn;
        /// <summary>
        /// 进入状态时调用
        /// </summary>
        public override void Enter(L_Root root)
        {
            m_Asyn = SceneManager.LoadSceneAsync("Initilize");
		}

        /// <summary>
        /// 状态更新时每帧调用
        /// </summary>
        public override void Execute(L_Root root)
        {
            if(m_Asyn.isDone) L_Root.ChangeState(GameState.GS_Menu);
		}


        /// <summary>
        /// 退出状态时调用
        /// </summary>
        public override void Exit(L_Root root) { }
    }

    /// <summary>
    /// 游戏界面状态
    /// </summary>
    public class L_GameStateMenu : IState<L_Root>
    {
        AsyncOperation m_Asyn;
        /// <summary>
        /// 进入状态时调用
        /// </summary>
        public override void Enter(L_Root root) {
            m_Asyn = SceneManager.LoadSceneAsync("Menu");
		}

        /// <summary>
        /// 状态更新时每帧调用
        /// </summary>
        public override void Execute(L_Root root) {
            if (L_SystemManager.Instance.ExistSystem(SystemType.ST_Menu)) return;
            if (m_Asyn.isDone) L_SystemManager.Instance.CreateSystem(SystemType.ST_Menu);
        }


        /// <summary>
        /// 退出状态时调用
        /// </summary>
        public override void Exit(L_Root root)
        { 
			// 移除系统
			L_SystemManager.Instance.RemoveSystem(SystemType.ST_Menu);
		}
    }

    /// <summary>
    /// 游戏状态
    /// </summary>
    public class L_GameStatePlay : IState<L_Root>
    {
        AsyncOperation m_Asyn;

        /// <summary>
        /// 进入状态时调用
        /// </summary>
        public override void Enter(L_Root root) {
            m_Asyn = SceneManager.LoadSceneAsync("Play");
		}

        /// <summary>
        /// 状态更新时每帧调用
        /// </summary>
        public override void Execute(L_Root root) {
            if (L_SystemManager.Instance.ExistSystem(SystemType.ST_Play)) return;
            if (m_Asyn.isDone) L_SystemManager.Instance.CreateSystem(SystemType.ST_Play);
        }

        /// <summary>
        /// 退出状态时调用
        /// </summary>
        public override void Exit(L_Root root) {
			L_SystemManager.Instance.RemoveSystem(SystemType.ST_Play);
		}
    }
}
