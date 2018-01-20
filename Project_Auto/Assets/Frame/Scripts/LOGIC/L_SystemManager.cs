/*******************************************************************************************************************
 * 作者：杜凯
 * 时间：2016.10.16
 * *****************************************************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TOOL;

namespace GameLogic{

	/// <summary>
	/// 系统管理器，系统工厂
	/// </summary>
	public abstract class L_SystemManager : U3DSingleton<L_SystemManager> {

		Dictionary<SystemType, L_System> m_Systems = new Dictionary<SystemType, L_System>(); // 系统列表

		//
		void Awake(){
			// 如果已经初始化了，则不执行一下内容
			if(!IsSingle) return;
            // 系统注册
            RegisterSystems();
        }

		//
		void OnDestroy(){
			// 如果已经初始化了，则不执行一下内容
			if(!IsSingle) return;
		}
		
		/// <summary>
		/// 更新系统
		/// </summary>
		public void CustomUpdate () {
            // 不可以在迭代的时候修改字典，严谨在L_System.CustomUpdate()中删除系统的操作
            foreach (L_System sys in m_Systems.Values) {
                sys.CustomUpdate();
            }
		}

        /// <summary>
        /// 创建系统
        /// </summary>
        /// <param name="type">系统类型</param>
        /// <returns>系统</returns>
		public L_System CreateSystem(SystemType type){

            if(m_Systems.ContainsKey(type))
				return m_Systems[type];

			L_System sys = IFactory<L_System>.Create((int)type);
			m_Systems.Add(type, sys);
			sys.Start();
			return sys;
		}

        /// <summary>
        /// 通过系统的种类移除系统
        /// </summary>
        /// <param name="type"></param>
		public void RemoveSystem(SystemType type) {
			if(m_Systems.ContainsKey(type)) {
				m_Systems[type].End();
				m_Systems.Remove(type);
			}
		}

        /// <summary>
        /// 系统是否存在
        /// </summary>
        public bool ExistSystem(SystemType type){
            foreach (KeyValuePair<SystemType, L_System> pair in m_Systems)
            {
                if (pair.Key == type) return true;
            }
            return false;
        }

        /// <summary>
        /// 注册所有系统
        /// </summary>
        protected abstract void RegisterSystems();
        
	}
}