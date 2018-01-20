using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameCommon;
using TOOL;

namespace GameLogic{
	/// <summary>
	/// 用户，负责记录用户数据/操作
	/// </summary>
	public abstract class L_Player : MonoBehaviour, IAttributeContainer {

		/// <summary>
		/// Gets the ID
		/// </summary>
		/// <value>The I.</value>
		public uint UserID{ get{ return m_ID;}}
		protected uint m_ID = 0;

		/// <summary>
		/// The attribute root.
		/// </summary>
		protected L_Attribute m_AttributeRoot = new L_Attribute();

		void Awake(){ 
			m_AttributeRoot.Key = "AttributeRoot";
			m_AttributeRoot.Value = "AttributeRoot";

			m_ID = L_PlayerManager.It.Add(this);
			name = "__Player_" + m_ID;

			Birth();
		}

		void OnDestroy(){
			Dead();

			L_PlayerManager.It.Remove(m_ID);
		}

		/// <summary>
		/// 获得属性
		/// </summary>
		/// <returns>The attribure.</returns>
		/// <param name="attName">Att name.</param>
		public L_Attribute GetAttribute(string name){
			return m_AttributeRoot.FindChild(name);
		}

		/// <summary>
		/// 初始化时调用
		/// </summary>
		protected abstract void Birth();

		/// <summary>
		/// 销毁时调用
		/// </summary>
		protected abstract void Dead();

		/// <summary>
		/// 更新，控制
		/// </summary>
		public abstract void CustomUpdate();
	}


	/// <summary>
	/// 用户管理器
	/// </summary>
	public class L_PlayerManager : Singleton<L_PlayerManager>
	{
		List<L_Player> m_Players = new List<L_Player>();
        
        /// <summary>
        /// 当前用户个数
        /// </summary>
        public int PlayerCount { get { return m_Players.Count; } }

		/// <summary>
		/// 添加角色
		/// </summary>
		/// <param name="actor">Actor.</param>
		public uint Add(L_Player player){
			m_Players.Add(player);
            return (uint)m_Players.Count;
		}
		
		/// <summary>
		/// 移除角色
		/// </summary>
		/// <param name="id">角色ID.</param>
		public void Remove(uint id){
			L_Player player = Find (id);
			if(player != null) m_Players.Remove(player);
		}

		/// <summary>
		/// 查找角色
		/// </summary>
		/// <param name="id">角色ID</param>
		public L_Player Find(uint id){
			L_Player player = null;
			for(int i = 0;i<m_Players.Count;i++){
				player = m_Players[i];
				if(player.UserID == id) break;
			}
			return player;
		}

        /// <summary>
        /// 清除所有角色
        /// </summary>
        public void Clear() {
            for (int i = m_Players.Count - 1; i >= 0; i--) {
                L_Player player = m_Players[i];
                GameObject.Destroy(player.gameObject);
            }
        }

		/// <summary>
		/// 更新角色
		/// </summary>
		public void CustomUpdate(){
			for(int i = 0;i<m_Players.Count;i++){
				m_Players[i].CustomUpdate();
			}
		}

		/// <summary>
		/// 创建一个玩家
		/// </summary>
		/// <returns>The player.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T CreatePlayer<T>() where T : L_Player {
			GameObject obj = new GameObject("__Player_");
			return obj.AddComponent<T>();
		}
	}
}