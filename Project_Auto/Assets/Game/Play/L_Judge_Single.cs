using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOOL;

namespace GameLogic
{
    /// <summary>
    /// 游戏规则
    /// </summary>
    public class L_Judge_Single : MonoBehaviour, IJudge{

        void Awake() {
            InitStateMachine();
        }

        void Update(){
            m_stateMachine.Update();
        }

        /// <summary>
        /// 结束触发器
        /// </summary>
        /// <returns>true：游戏结束</returns>
        public bool EndTrigger() {
            return false;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        protected virtual void CreatePlayer() {
            L_PlayerManager.It.CreatePlayer<L_Player_User>();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        protected virtual void LoadData() {
            // 装载数据(CSV文件)
            //L_Data root = L_DataPool.Instance.FindChild("GamePlay");
            //if (root != null) root.ClearChildren();
            //else root = L_DataPool.Instance.CreatChildData("GamePlay", "GamePlay");
            //root = CSVTool.LoadCsv<L_Data>(root,"Test"); // CSV数据
            // ...
        }

        /// <summary>
        /// 玩家状态
        /// </summary>
        IStateMachine<L_Judge_Single> m_stateMachine;

        ///----------------------------------------------------------------------------------
        ///1. 定义状态类型枚举
        ///2. 创建状态类型
        ///3. 注册状态
        ///----------------------------------------------------------------------------------
        
        /// <summary>
        /// 游戏系统状态类型
        /// </summary>
        enum PlayState
        {
            PS_Initialize,	// 游戏初始状态
            PS_Ready,	    // 准备状态
            PS_Playing,     // 游戏状态
            PS_End,         // 结束状态
        }

        /// <summary>
        /// 初始化状态机
        /// </summary>
        void InitStateMachine()
        {
            m_stateMachine = new IStateMachine<L_Judge_Single>(this);
            m_stateMachine.Add(PlayState.PS_Initialize, new PlayState_Initilize());
            m_stateMachine.Add(PlayState.PS_Ready, new PlayState_Ready());
            m_stateMachine.Add(PlayState.PS_Playing, new PlayState_Playing());
            m_stateMachine.Add(PlayState.PS_End, new PlayState_End());
            // ...
            m_stateMachine.ChangeState(PlayState.PS_Initialize); // 设置默认状态
        }


        /// <summary>
        /// 初始化状态
        /// </summary>
        class PlayState_Initilize : IState<L_Judge_Single>
        {
            public override void Enter(L_Judge_Single root)
            {
                root.LoadData();        // 装载数据
                root.CreatePlayer();    // 创建角色
            }
            public override void Execute(L_Judge_Single root)
            {
                // 必须两个角色才能开始游戏
                if (L_PlayerManager.It.PlayerCount > 0) root.m_stateMachine.ChangeState(PlayState.PS_Ready);
            }
        }

        /// 准备状态
        /// </summary>
		class PlayState_Ready : IState<L_Judge_Single>
        {
            public override void Enter(L_Judge_Single root)
            {
                // 倒计时3秒开始
                ClockMachine.It.CreateClock(0.5f, () => { root.m_stateMachine.ChangeState(PlayState.PS_Playing); });
            }
        }

        /// <summary>
        /// 游戏状态
        /// </summary>
		class PlayState_Playing : IState<L_Judge_Single> {}

        /// <summary>
        /// 结束状态
        /// </summary>
		class PlayState_End : IState<L_Judge_Single> {}
    }
}