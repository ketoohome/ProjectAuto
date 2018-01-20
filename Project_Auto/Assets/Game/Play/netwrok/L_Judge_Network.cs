using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TOOL;

namespace GameLogic
{
    /// <summary>
    /// 游戏规则
    /// </summary>
    public class L_Judge_Network : NetworkBehaviour, IJudge
    {

        static L_Judge_Network m_Instance = null;
        /// <summary>
        /// 单件类型
        /// </summary>
        static public L_Judge_Network Instance{ get{return m_Instance;}}


        void Awake() {
            m_Instance = this;
            CreateNetworkMgr();
            InitStateMachine();
        }

        void OnDestroy() {
        }

        void Update() {
            m_stateMachine.Update();
        }

        /// <summary>
        /// 结束触发器
        /// </summary>
        /// <returns>true：游戏结束</returns>
        public bool EndTrigger()
        {
            return false;
        }

        /// <summary>
        /// 创建网络管理器
        /// </summary>
        void CreateNetworkMgr()
        {
            GameObject netPrefab = Resources.Load<GameObject>("Other/NetworkManager");
            GameObject net = GameObject.Instantiate<GameObject>(netPrefab);
            net.name = "__NetworkManager";
        }

        ///----------------------------------------------------------------------------------
        ///1. 定义状态类型枚举
        ///2. 创建状态类型
        ///3. 注册状态
        ///----------------------------------------------------------------------------------

        /// <summary>
        /// 玩家状态
        /// </summary>
        IStateMachine<L_Judge_Network> m_stateMachine;

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
            m_stateMachine = new IStateMachine<L_Judge_Network>(this);
            m_stateMachine.Add(PlayState.PS_Initialize, new PlayState_Initilize());
            m_stateMachine.Add(PlayState.PS_Ready, new PlayState_Ready());
            m_stateMachine.Add(PlayState.PS_Playing, new PlayState_Playing());
            m_stateMachine.Add(PlayState.PS_End, new PlayState_End());
            // ...
            m_stateMachine.ChangeState(PlayState.PS_Initialize); // 设置默认状态
        }

        class PlayState_Initilize : IState<L_Judge_Network>{ }
		class PlayState_Ready : IState<L_Judge_Network>{}
		class PlayState_Playing : IState<L_Judge_Network> {}
		class PlayState_End : IState<L_Judge_Network> {}
    }
}