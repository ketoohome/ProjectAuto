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
            m_stateMachine = new IQStateMachine<L_Judge_Single, State>(this);
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
        IQStateMachine<L_Judge_Single,State> m_stateMachine;

        ///----------------------------------------------------------------------------------
        ///1. 定义状态类型枚举
        ///2. 创建状态类型
        ///3. 注册状态
        ///----------------------------------------------------------------------------------
        
        /// <summary>
        /// 游戏系统状态类型
        /// </summary>
        enum State
        {
            Initialize,	 // 游戏初始状态
            Ready,	     // 准备状态
            Playing,     // 游戏状态
            End,         // 结束状态
        }

        /// <summary>
        /// 初始化状态
        /// </summary>
        class StateInitialize : IQState<L_Judge_Single>
        {
            public override void Enter()
            {
                Root.LoadData();        // 装载数据
                Root.CreatePlayer();    // 创建角色
            }
            public override void Execute()
            {
                // 必须两个角色才能开始游戏
                if (L_PlayerManager.It.PlayerCount > 0) Root.m_stateMachine.ChangeState(State.Ready);
            }
        }

        /// 准备状态
        /// </summary>
		class StateReady : IQState<L_Judge_Single>
        {
            public override void Enter()
            {
                // 倒计时3秒开始
                ClockMachine.It.CreateClock(0.5f, () => { Root.m_stateMachine.ChangeState(State.Playing); });
            }
        }

        /// <summary>
        /// 游戏状态
        /// </summary>
		class StatePlaying : IQState<L_Judge_Single> {}

        /// <summary>
        /// 结束状态
        /// </summary>
		class StateEnd : IQState<L_Judge_Single> {}
    }
}