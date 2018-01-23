using UnityEngine;
using System.Collections;
using TOOL;
using GameCommon;

namespace GameLogic
{
    /// <summary>
    /// 游戏主逻辑
    /// </summary>
    public class L_Root : U3DSingleton<L_Root>
    {
        /// <summary>
        /// 游戏状态
        /// </summary>
		GameState m_GameState = GameState.GS_Initialize;
        /// <summary>
        /// 游戏当前状态
        /// </summary>
        public GameState State { get { return m_GameState; } }
        /// <summary>
        /// 游戏状态机实例
        /// </summary>
        protected IStateMachine<L_Root> m_stateMachine;

        /// <summary>
        /// 改变游戏当前状态
        /// </summary>
        /// <param name="state">改变的状态</param>
        static public void ChangeState(GameState state) {
            Instance.m_GameState = state;
            Instance.m_stateMachine.ChangeState(state);
        }

        /// <summary>
        /// 游戏初始化
        /// </summary>
        void Awake() {
            // 防止重复创建
            if (!IsSingle) { Destroy(gameObject); return; }
            // 主逻辑所挂接的gameObject不能在切换场景的时候删除。
            DontDestroyOnLoad(gameObject);
            // 初始化系统管理器(注册器)
            gameObject.AddComponent<L_SystemRegister>();
            // 初始化缓存池，创建一个根节点
            gameObject.AddComponent<L_DataPool>();
            // 初始化排行榜管理器
            gameObject.AddComponent<L_RankManager>();
            // 初始化状态机
            m_stateMachine = new IStateMachine<L_Root>(this);
            // 注册状态
            RegisterStates();
            // 防止忘记设置初始化状态
            if (m_stateMachine.CurrentState() == null) {
                Debuger.LogError("当前状态为NULL，请在注册函数中设置初始化状态！");
            }
            // 游戏逻辑初始化完成，游戏启动
            EventMachine.SendEvent(EventID.Event_GameLogicStart);				
        }

        /// <summary>
        /// 游戏更新
        /// </summary>
        void Update() {
            if (!m_IsLogicPause) {
                // 系统更新
                L_SystemRegister.Instance.CustomUpdate();
                // 游戏状态更新
                m_stateMachine.Update();
                // 计时器更新
                ClockMachine.It.CustomUpdate(Time.deltaTime);
                // 计数器更新
                CounterMachine.It.CustomUpdate();
            }
            // 实时计时器更新
            ClockMachine.It.RealTimeUpdate(Time.unscaledDeltaTime);
        }

        bool m_IsLogicPause = false;
        /// <summary>
        /// 游戏是否暂停
        /// </summary>
        public bool LogicPause { get { return m_IsLogicPause; } set { m_IsLogicPause = value; } }

        /// <summary>
        /// 注册游戏状态机状态
        /// </summary>
		protected virtual void RegisterStates(){}
    }
}